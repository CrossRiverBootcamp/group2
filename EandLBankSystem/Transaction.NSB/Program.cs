
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NServiceBus;
using NServiceBus.Logging;
using Transaction.Service;

class Program
{
    static ILog log = LogManager.GetLogger<Program>();

    static async Task Main()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile($"appsettings.json");

        var config = configuration.Build();
        var databaseConnection = config.GetConnectionString("TransactionDatabase");
        var queueName = config.GetSection("Queues:TransactionQueue:Name").Value;

        Console.Title = queueName;
        var endpointConfiguration = new EndpointConfiguration(queueName);

        #region Receiver Configuration

        var rabbitMQConnection = config.GetConnectionString("RabbitMQ");
        var NSBConnection = config.GetConnectionString("NSBConnection");

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

        //var conventions = endpointConfiguration.Conventions();
        //conventions.DefiningEventsAs(type => type.Namespace == "Subscriber.Messages.Events");

        #endregion

        var containerSettings = endpointConfiguration.UseContainer(new DefaultServiceProviderFactory());
        containerSettings.ServiceCollection.AddServicesExtention(databaseConnection);
        containerSettings.ServiceCollection.AddScoped<ITransactionService, TransactionService>();
        containerSettings.ServiceCollection.AddAutoMapper(typeof(Program));

        var endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);

        Console.WriteLine("waiting for messages...");
        Console.ReadLine();

        await endpointInstance.Stop();
    }
}