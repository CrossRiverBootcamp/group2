using Account.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
namespace Account.Data;

public class AccountDal : IAccountDal
{
    private readonly ILogger<AccountDal> _logger;
    private readonly IDbContextFactory<AccountContext> _factory;
    public AccountDal(IDbContextFactory<AccountContext> factory, ILogger<AccountDal> logger)
    {
        _logger = logger;
        _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        using var db = _factory.CreateDbContext();
        db.Database.Migrate();
    }

    #region Customer
    public async Task<Customer> getCustomerByEmailAsync(string email)
    {
        using var db = _factory.CreateDbContext();
        return await db.Customers
            .FirstOrDefaultAsync(c => c.Email.Equals(email)) ?? throw new UnauthorizedAccessException("The requested user was not found.");
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
    #endregion
    #region Account
    public async Task<Entities.Account> GetAccountInfoAsync(int id)
    {
        using var db = _factory.CreateDbContext();
        Entities.Account accountFound = await db.Accounts
            .Where(a => a.Id == id).Include(a=> a.Customer)
            .FirstOrDefaultAsync() ?? throw new KeyNotFoundException($"Internal server error: no record was found for '{nameof(id)}' {id}");
        return accountFound;
    }
    public async Task SignUpAsync(Customer customer)
    {
        if (await EmailAddressExistsAsync(customer.Email))
            throw new ArgumentException("Email already exists", customer.Email);
        using var db = _factory.CreateDbContext();
        await db.Customers.AddAsync(customer);
        await db.Accounts.AddAsync(new() 
        {
            OpenDate = DateTime.UtcNow,
            Customer = customer
        });
        await db.SaveChangesAsync();  
    }
    public async Task<bool> EmailAddressExistsAsync(string email)
    {
        using var db = _factory.CreateDbContext();
        return await db.Customers.AnyAsync(c => c.Email.Equals(email));
    }
    public async Task TransferAmountAsync(Operation operationHistoryfrom, Operation operationHistoryfromTo, int amount)
    {
        using var db = _factory.CreateDbContext();
        Task<Entities.Account?> fromTask = db.Accounts.FirstOrDefaultAsync(a => a.Id == operationHistoryfrom.AccountId);
        Task<Entities.Account?> toTask = db.Accounts.FirstOrDefaultAsync(a => a.Id == operationHistoryfromTo.AccountId);

        List<Task> tasks = new();
        tasks.Add(fromTask);
        tasks.Add(toTask);
        await Task.WhenAll(tasks);
   
        if ( fromTask.Result == null || toTask.Result == null)
            throw new KeyNotFoundException("One or more accounts don't exist in db.");

        if (fromTask.Result.Balance < amount)
            throw new Exception("There is not enough balance in the account");

        fromTask.Result.Balance -= amount;
        toTask.Result.Balance += amount;

        tasks.Add(db.Operations.AddAsync(operationHistoryfromTo).AsTask());
        tasks.Add(db.Operations.AddAsync(operationHistoryfrom).AsTask());
        await Task.WhenAll(tasks);

        await db.SaveChangesAsync();
    }
    #endregion
    #region Operation

    public async Task<List<OperationSecondSideModel>> GetOperationsByAccountIdAsync(int accountId, int currentPage, int pageSize)
    {
        using var db = _factory.CreateDbContext();

        var sqlQuery =
            from op1 in db.Operations
            join op2 in db.Operations on op1.TransactionId equals op2.TransactionId
            where op1.AccountId == accountId && op1.Id != op2.Id
            orderby op1.OperationTime descending
            select new OperationSecondSideModel
            {
                Id = op1.Id,
                SecondSideAccountId = op2.AccountId,
                TransactionId = op1.TransactionId,
                Credit = op1.Credit,
                TransactionAmount = op1.TransactionAmount,
                Balance = op1.Balance,
                OperationTime = op1.OperationTime
            };

        if (currentPage == 0)
            return await sqlQuery.Skip(currentPage * pageSize).Take(pageSize + 1).ToListAsync();
        return await sqlQuery.Skip(currentPage * pageSize + 1).Take(pageSize).ToListAsync();
    }
    #endregion
    #region EmailVerification
    public async Task AddEmailVerificationAsync(EmailVerification emailVerification)
    {
        using var db = _factory.CreateDbContext();
        await db.EmailVerifications.AddAsync(emailVerification);
        await db.SaveChangesAsync();
    }
    public async Task<EmailVerification?> GetEmailVerificationAsync(string email)
    {
        using var db = _factory.CreateDbContext();
        return await db.EmailVerifications.FirstOrDefaultAsync(ev => ev.Email.Equals(email));
    }
    public async Task RemoveEmailVerificationAsync(EmailVerification verification)
    {
        using var db = _factory.CreateDbContext();
        db.EmailVerifications.Remove(verification);
        await db.SaveChangesAsync();
    }
    public async Task IncreaseNumOfTriesAsync(string email)
    {
        var verification = await GetEmailVerificationAsync(email);
        if (verification != null)
        {
            verification.NumOfTries++;
            using var db = _factory.CreateDbContext();
            await db.SaveChangesAsync();
        }
    }
    #endregion

    #region SendEmailTrack
    public async Task<SendEmailTrack?> GetSendEmailTrackAsync(string email)
    {
        using var db = _factory.CreateDbContext();
        return await db.SendEmailTracks.FirstOrDefaultAsync(set => set.Email.Equals(email));
    }

    public async Task AddSendEmailTrackAsync(SendEmailTrack set)
    {
        using var db = _factory.CreateDbContext();
        await db.SendEmailTracks.AddAsync(set);
        await db.SaveChangesAsync();
    }

    public async Task UpdateSendEmailTrackAsync(SendEmailTrack set)
    {
        using var db = _factory.CreateDbContext();
        SendEmailTrack setToUpdate =  await db.SendEmailTracks
            .FirstOrDefaultAsync(set => set.Email.Equals(set.Email)) ?? throw new KeyNotFoundException("No such sendEmailTrack");
        db.Entry(setToUpdate).CurrentValues.SetValues(set);
        await db.SaveChangesAsync();
    }

    public async Task RemoveSendEmailTrackAsync(string email)
    {
        using var db = _factory.CreateDbContext();
        SendEmailTrack? setToDelete = await db.SendEmailTracks
         .FirstOrDefaultAsync(set => set.Email.Equals(set.Email));
        db.Remove(setToDelete);
        await db.SaveChangesAsync();
    }
    #endregion
}