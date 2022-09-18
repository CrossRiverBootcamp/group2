using Transaction.Data;
using Transaction.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
namespace Transaction.Service;

public static class TransactionExtention
{

    public static void AddServicesExtention(this IServiceCollection services,string connectionString)
    {
        services.AddScoped<ITransactionDal, TransactionDal>();
        services.AddDbContextFactory<TransactionContext>(opt => opt.UseSqlServer(connectionString));
        using (var scope = services.BuildServiceProvider().CreateScope())
        {
            var dataContext = scope.ServiceProvider.GetRequiredService<TransactionContext>();
            dataContext.Database.Migrate();
        }
    }

}

