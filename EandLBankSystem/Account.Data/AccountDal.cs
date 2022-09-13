using Account.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Account.Data;

public class AccountDal:IAccountDal
{
    private readonly ILogger<AccountDal> _logger;
    private IDbContextFactory<AccountContext> _factory;
    public AccountDal(IDbContextFactory<AccountContext> factory , ILogger<AccountDal> logger)
    {
        _logger = logger;
        _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        using var db = _factory.CreateDbContext();
        db.Database.Migrate();
    }

    public async Task<string> getCustomerByEmailAsync(string email)
    {
        using var db = _factory.CreateDbContext();
        Customer customerFound = await db.Customers
            .FirstOrDefaultAsync(c => c.Email.Equals(email)) ?? throw new UnauthorizedAccessException("The requested user was not found."); 
        return customerFound.Salt;
    }
    public async Task<int> SignInAsync(string email, string password)
    {
        using var db = _factory.CreateDbContext();
        Customer customerFound = await db.Customers
            .FirstOrDefaultAsync(c => c.Email.Equals(email)
                                   && c.Password.Equals(password)) ?? throw new UnauthorizedAccessException("The requested user was not found.");
        Entities.Account accountFound = await db.Accounts
            .FirstOrDefaultAsync(a => a.CustomerId == customerFound.Id) ?? throw new Exception("Internal Server Error.");
        return accountFound.Id;
    }
    public async Task<Entities.Account> GetAccountInfoAsync(int id)
    {
        using var db = _factory.CreateDbContext();
        Entities.Account accountFound = await db.Accounts
            .Where(a=>a.Id==id).Include(a=> a.Customer)
            .FirstOrDefaultAsync() ?? throw new KeyNotFoundException(nameof(id));
        return accountFound;
    }
    public async Task<bool> SignUpAsync(Customer customer)
    {
        if(await EmailAddressExistsAsync(customer.Email))  
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
        catch(Exception ex)
        {
            _logger.LogError(ex.Message, ex.StackTrace);
            return false;
        }
        return true;
    }
    public async Task<bool> EmailAddressExistsAsync(string email)
    {
       using var db = _factory.CreateDbContext();
       return await db.Customers.AnyAsync(c => c.Email.Equals(email));  
    }
    public async Task TransferAmountAsync(int fromAccount, int toAccount, int amount)
    {
        using var db = _factory.CreateDbContext();
        var from = await db.Accounts.FirstOrDefaultAsync(a => a.Id == fromAccount);
        var to = await db.Accounts.FirstOrDefaultAsync(a => a.Id == toAccount);

        if (from == null || to == null)
            throw new KeyNotFoundException("One or more accounts don't exist in db.");

        if (from.Balance < amount)
            throw new Exception("There is not enough balance in the account");

        from.Balance -= amount;
        to.Balance += amount;

        await db.SaveChangesAsync();   
    }
    public async Task AddNewOperationAsync(Operation operationHistoryfrom, Operation operationHistoryfromTo)
    {
        using var db = _factory.CreateDbContext();
        await db.Operations.AddAsync(operationHistoryfromTo);
        await db.Operations.AddAsync(operationHistoryfrom);

        await db.SaveChangesAsync();
    }
    public async Task<List<OperationSecondSideModel>> GetOperationsByAccountIdAsync(int accountId, int position , int pageSize)
    {
        using var db = _factory.CreateDbContext();

        var sqlQuery =
            from op1 in db.Operations
            join op2 in db.Operations on op1.TransactionId equals op2.TransactionId
            where op1.AccountId == accountId && op1.Id != op2.Id
            orderby op1.OperationTime descending
            select new OperationSecondSideModel{ 
            Id = op1.Id
          , SecondSideAccountId = op2.AccountId
          , TransactionId = op1.TransactionId
          , Credit = op1.Credit
          , TransactionAmount = op1.TransactionAmount
          , Balance = op1.Balance
          , OperationTime = op1.OperationTime }
        ;

        return sqlQuery.Skip(position).Take(pageSize).ToList();
    }
}