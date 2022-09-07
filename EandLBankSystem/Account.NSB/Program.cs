
using Account.Messages.Commands;
using Account.Service;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NServiceBus;

class Program
{
    static async Task Main()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile($"appsettings.json");

        var config = configuration.Build();
        var databaseConnection = config.GetConnectionString("AccountDatabase");
        var queueName = config.GetSection("Queues:AccountQueue:Name").Value;
        
        Console.Title = queueName;
        var endpointConfiguration = new EndpointConfiguration(queueName);

        #region Receiver Configuration

        var rabbitMQConnection = config.GetConnectionString("RabbitMQ");
        var NSBConnection = config.GetConnectionString("NSBConnection");

        endpointConfiguration.EnableInstallers();
        endpointConfiguration.EnableOutbox();

        var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
        transport.ConnectionString(rabbitMQConnection);
        transport.UseConventionalRoutingTopology(QueueType.Quorum);

        var routing = transport.Routing();
        routing.RouteToEndpoint(typeof(FinishTransactionSaga), "Transaction");

        var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
        persistence.ConnectionBuilder(
            connectionBuilder: () =>
            {
                return new SqlConnection(NSBConnection);
            });

        var dialect = persistence.SqlDialect<SqlDialect.MsSqlServer>();

        //var conventions = endpointConfiguration.Conventions();
        //conventions.DefiningEventsAs(type => type.Namespace == "Subscriber.Messages.Events");

        #endregion

        var containerSettings = endpointConfiguration.UseContainer(new DefaultServiceProviderFactory());
        containerSettings.ServiceCollection.AddServicesExtention(databaseConnection);
        containerSettings.ServiceCollection.AddScoped<IAccountService, AccountService>();
        containerSettings.ServiceCollection.AddAutoMapper(typeof(Program));

        var endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);

        Console.WriteLine("waiting for messages...");
        Console.ReadLine();

        await endpointInstance.Stop();
    }
}