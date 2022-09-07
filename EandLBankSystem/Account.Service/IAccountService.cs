using Account.Service.Models;

namespace Account.Service;

public interface IAccountService
{
    Task<int> SignIn(string email, string password);
    Task<AccountModel> GetAccountInfo(int id);
    Task<bool> SignUp(CustomerModel customerModel);
    Task ExecuteTransaction(TransactionModel transactionModel);
}

