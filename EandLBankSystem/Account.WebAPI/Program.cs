using Account.Messages.Commands;
using Account.Service;
using Account.Service.Services;
using Account.WebAPI;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NServiceBus;


var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AccountDatabase");
#region NServiceBus configurations

var rabbitMQConnection = builder.Configuration.GetConnectionString("RabbitMQ");
var queueName = builder.Configuration.GetSection("Queues:AccountAPIQueue:Name").Value;
var NSBConnection = builder.Configuration.GetConnectionString("NSBConnection");

builder.Host.UseNServiceBus(hostBuilderContext =>
{
    var endpointConfiguration = new EndpointConfiguration(queueName);

    endpointConfiguration.EnableInstallers();
    endpointConfiguration.EnableOutbox();

    var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
    persistence.ConnectionBuilder(
        connectionBuilder: () =>
        {
            return new SqlConnection(NSBConnection);
        });
    var dialect = persistence.SqlDialect<SqlDialect.MsSqlServer>();

    var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
    transport.ConnectionString(rabbitMQConnection);
    transport.UseConventionalRoutingTopology(QueueType.Quorum);

    var routing = transport.Routing();
    routing.RouteToEndpoint(typeof(FinishTransactionSaga), "Transaction");
    //routing.RouteToEndpoint(typeof(DelayDeleteVerification), "Account");

    var conventions = endpointConfiguration.Conventions();
    conventions.DefiningCommandsAs(type => type.Namespace == "Account.Messages.Commands");
    conventions.DefiningEventsAs(type => type.Namespace == "Transaction.Messages.Events");

    return endpointConfiguration;
});

#endregion

#region Adding services to the container.

builder.Services.AddServicesExtention(connectionString);
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IOperationService, OperationService>();
builder.Services.AddScoped<IEmailVerificationService, EmailVerificationService>();
builder.Services.AddScoped<IPasswordHash, PasswordHash>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", p =>
    {
        p.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

#endregion

#region configuring Swagger

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "E&L.Account", Version = "v1" })
    );

#endregion

var app = builder.Build();

#region Adding middlewares

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "E&L.Account V1");
    });
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseHandleErrorMiddleware();

app.UseAuthorization();

app.MapControllers();

app.Run();

#endregion