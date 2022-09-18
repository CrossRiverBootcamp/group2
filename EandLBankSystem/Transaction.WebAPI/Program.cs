using Microsoft.Data.SqlClient;
using Microsoft.OpenApi.Models;
using NServiceBus;
using Transaction.Api.Middlewares;
using Transaction.Messages.Commands;
using Transaction.Service;

var builder = WebApplication.CreateBuilder(args);
var databaseConnection = builder.Configuration.GetConnectionString("TransactionDatabase");

#region NServiceBus configurations

var rabbitMQConnection = builder.Configuration.GetConnectionString("RabbitMQ");
var queueName = builder.Configuration.GetSection("Queues:TransactionAPIQueue:Name").Value;
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
    routing.RouteToEndpoint(typeof(StartTransactionSaga), "Transaction");

    var conventions = endpointConfiguration.Conventions();
    conventions.DefiningCommandsAs(type => type.Namespace == "Account.Messages.Commands");
    conventions.DefiningCommandsAs(type => type.Namespace == "Transaction.Messages.Commands");
    conventions.DefiningEventsAs(type => type.Namespace == "Transaction.Messages.Events");



    return endpointConfiguration;
});

#endregion

#region Adding services to the container.

builder.Services.AddServicesExtention(databaseConnection);
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddAutoMapper(typeof(Program));

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
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "E&L.Transaction", Version = "v1" })
   );

#endregion

var app = builder.Build();

#region Adding middlewares

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "E&L.Transaction V1");
    });
}


app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseHandleErrorMiddleware();

app.UseAuthorization();

app.MapControllers();

app.Run();

#endregion