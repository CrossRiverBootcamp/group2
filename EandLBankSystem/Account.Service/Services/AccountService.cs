using Account.Data;
using Account.Data.Entities;
using Account.Service.Models;
using Account.Service.Services;
using AutoMapper;

namespace Account.Service;

public class AccountService:IAccountService
{
    private readonly IAccountDal _accountDal;
    private readonly IMapper _mapper;
    private readonly IPasswordHash _passwordHash;
    private readonly IEmailVerificationService _emailVerificationService;
    private static readonly int N_SALT = 6;
    private static readonly int N_HASH = 9;
    private static readonly int N_ITERATIONS = 900;

    public AccountService(IAccountDal accountDal , IPasswordHash passwordHash ,IEmailVerificationService emailVerificationService)
    {
        _accountDal = accountDal;
        _mapper = ConfigureAutoMapper();
        _passwordHash = passwordHash;
        _emailVerificationService = emailVerificationService;
    }
   
    public async Task<int> SignInAsync(string email, string password)
    {
        string salt = (await _accountDal.getCustomerByEmailAsync(email)).Salt;
        password = _passwordHash.HashPassword(password, salt, N_ITERATIONS, N_HASH);
        return await _accountDal.SignInAsync(email, password);
    }
    public async Task<AccountModel> GetAccountInfoAsync(int id)
    {
         return _mapper.Map<AccountModel>(await _accountDal.GetAccountInfoAsync(id));
    }
    public async Task SignUpAsync(CustomerModel customerModel,string verificationCode)
    {
        await _emailVerificationService.VerifyEmailAsync(new() { Email = customerModel.Email, Code = verificationCode });
        string salt = _passwordHash.GenerateSalt(N_SALT);
        customerModel.Password = _passwordHash.HashPassword(customerModel.Password, salt , N_ITERATIONS , N_HASH);
        Customer customer = _mapper.Map<Customer>(customerModel);
        customer.Salt = salt;
        await _accountDal.SignUpAsync(customer);
        await _accountDal.RemoveSendEmailTrackAsync(customer.Email);
    }
    public async Task ExecuteTransactionAsync(TransactionModel transactionModel)
    {
        Data.Entities.Account fromAccount = await _accountDal.GetAccountInfoAsync(transactionModel.FromAccount);
        Operation fromOperationHistory = new()
        {
            AccountId = transactionModel.FromAccount,
            TransactionId = transactionModel.TransactionId,
            Credit = false,
            TransactionAmount = transactionModel.Amount,
            Balance = fromAccount.Balance- transactionModel.Amount,
            OperationTime = DateTime.UtcNow
        };

        Data.Entities.Account toAccount = await _accountDal.GetAccountInfoAsync(transactionModel.ToAccount);
        Operation toOperationHistory = new()
        {
            AccountId = transactionModel.ToAccount,
            TransactionId = transactionModel.TransactionId,
            Credit = true,
            TransactionAmount = transactionModel.Amount,
            Balance = toAccount.Balance+ transactionModel.Amount,
            OperationTime = DateTime.UtcNow
        };
        await _accountDal.TransferAmountAsync(fromOperationHistory,toOperationHistory, transactionModel.Amount);
    }

    
    private static IMapper ConfigureAutoMapper()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ServiceAutoMapper>();
        });
        return config.CreateMapper();
    }
}

