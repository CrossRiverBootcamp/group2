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
    public Task<int> SignIn(string email, string password)
    {
        return _accountDal.SignIn(email, password);
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

    private static IMapper ConfigureAutoMapper()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ServiceAutoMapper>();
        });
        return config.CreateMapper();
    }


}

