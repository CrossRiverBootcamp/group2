using Account.Data;
using Account.Data.Entities;
using Account.Service.Models;
using AutoMapper;

namespace Account.Service;

public class AccountService:IAccountService
{
    private readonly IAccountDal _accountDal;
    private readonly IMapper _mapper;

    public AccountService(IAccountDal accountDal)
    {
        _accountDal = accountDal;
        _mapper = ConfigureAutoMapper();
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
        return _accountDal.SignUp(_mapper.Map<Customer>(customerModel));
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

