using Account.Data;
using Account.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Account.Service;

public static class ServicesExtention
{
    public static void AddServicesExtention(this IServiceCollection services,string connectionString)
    {
        services.AddScoped<IAccountDal, AccountDal>();
        services.AddDbContextFactory<AccountContext>(opt => opt.UseSqlServer(connectionString));
    }
}

