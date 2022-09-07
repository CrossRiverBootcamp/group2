using Microsoft.OpenApi.Models;
using NServiceBus;
using Transaction.Messages.Commands;
using Transaction.Service;

var builder = WebApplication.CreateBuilder(args);
var databaseConnection = builder.Configuration.GetConnectionString("TransactionDatabase");

#region nservicebus configurations
var rabbitMQConnection = builder.Configuration.GetConnectionString("RabbitMQ");
var queueName = builder.Configuration.GetSection("Queues:TransactionAPIQueue:Name").Value;

builder.Host.UseNServiceBus(hostBuilderContext =>
{
    var endpointConfiguration = new EndpointConfiguration(queueName);
    endpointConfiguration.SendOnly();

    var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
    transport.ConnectionString(rabbitMQConnection);
    transport.UseConventionalRoutingTopology(QueueType.Quorum);

    var routing = transport.Routing();
    routing.RouteToEndpoint(typeof(StartTransactionSaga), "Transaction");

    return endpointConfiguration;
});

#endregion

// Adding services to the container.
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

// configuring Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "E&L.Transaction", Version = "v1" })
   );

var app = builder.Build();

// Configure the HTTP request pipeline.
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

app.UseAuthorization();

app.MapControllers();

app.Run();
