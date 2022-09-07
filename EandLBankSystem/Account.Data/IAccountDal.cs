using Account.Data.Entities;

namespace Account.Data;

public interface IAccountDal
{
    Task<string> getCustomerByEmail(string email);
    Task<int> SignIn(string email, string password);
    Task<Entities.Account> GetAccountInfo(int id);
    Task<bool> SignUp(Customer customer);
    Task TransferAmmount(int FromAccount, int toAccount, int ammount);

}

