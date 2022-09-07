using Account.Data;
using Account.Data.Entities;
using Account.Service.Models;
using AutoMapper;

namespace Account.Service;

public class AccountService:IAccountService
{
    private readonly IAccountDal _accountDal;
    private readonly IMapper _mapper;
    private readonly IPasswordHash _passwordHash;
    private static readonly int N_SALT = 6;
    private static readonly int N_HASH = 9;
    private static readonly int N_ITERATIONS = 900;

    public AccountService(IAccountDal accountDal , IPasswordHash passwordHash)
    {
        _accountDal = accountDal;
        _mapper = ConfigureAutoMapper();
        _passwordHash = passwordHash;
    }
    public async Task<int> SignIn(string email, string password)
    {
        string salt = await _accountDal.getCustomerByEmail(email);
        password = _passwordHash.HashPassword(password, salt, N_ITERATIONS, N_HASH);
        return await _accountDal.SignIn(email, password);
    }
    public async Task<AccountModel> GetAccountInfo(int id)
    {
         return _mapper.Map<AccountModel>(await _accountDal.GetAccountInfo(id));
    }
    public Task<bool> SignUp(CustomerModel customerModel)
    {
        string salt = _passwordHash.GenerateSalt(N_SALT);
        customerModel.Password = _passwordHash.HashPassword(customerModel.Password, salt , N_ITERATIONS , N_HASH);
        Customer customer = _mapper.Map<Customer>(customerModel);
        customer.Salt = salt;
        return _accountDal.SignUp(customer);
    }
    public async Task ExecuteTransaction(TransactionModel transactionModel)
    {
        try {       
            await _accountDal.TransferAmmount(transactionModel.FromAccount, transactionModel.ToAccount, transactionModel.Ammount);
        }
        catch
        {
            throw;
        }
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

