using Account.Data.Entities;

using Microsoft.EntityFrameworkCore;

namespace Account.Data;

public class AccountDal:IAccountDal
{
    private IDbContextFactory<AccountContext> _factory;

    public AccountDal(IDbContextFactory<AccountContext> factory)
    {
        _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        using var db = _factory.CreateDbContext();
        db.Database.Migrate();
    }
    public async Task<string> getCustomerByEmail(string email)
    {
        using var db = _factory.CreateDbContext();
        Customer customerFound = await db.Customers
            .FirstOrDefaultAsync(c => c.Email.Equals(email)) ?? throw new UnauthorizedAccessException("The requested user was not found."); 
        return customerFound.Salt;
    }
    public async Task<int> SignIn(string email, string password)
    {
        using var db = _factory.CreateDbContext();
        Customer customerFound = await db.Customers
            .FirstOrDefaultAsync(c => c.Email.Equals(email)
                                   && c.Password.Equals(password)) ?? throw new UnauthorizedAccessException("The requested user was not found.");
        Entities.Account accountFound = await db.Accounts
            .FirstOrDefaultAsync(a => a.CustomerId == customerFound.Id) ?? throw new Exception("Internal Server Error.");
        return accountFound.Id;
    }
    public async Task<Entities.Account> GetAccountInfo(int id)
    {
        using var db = _factory.CreateDbContext();
        Entities.Account accountFound = await db.Accounts
            .Where(a=>a.Id==id).Include(a=> a.Customer)
            .FirstOrDefaultAsync() ?? throw new KeyNotFoundException(nameof(id));
        return accountFound;
    }

    public async Task<bool> SignUp(Customer customer)
    {
        if(await EmailAddressExists(customer.Email))  
            throw new ArgumentException(nameof(customer));
        using var db = _factory.CreateDbContext();
        await db.Customers.AddAsync(customer);
        await db.Accounts.AddAsync(new Entities.Account()
        {
            OpenDate = DateTime.UtcNow,
            Customer = customer
        });
        try
        {
            await db.SaveChangesAsync();
        }
        catch
        {
            return false;
        }
        return true;
    }
    public async Task<bool> EmailAddressExists(string email)
    {
       using var db = _factory.CreateDbContext();
       return await db.Customers.AnyAsync(c => c.Email.Equals(email));  
    }
}

