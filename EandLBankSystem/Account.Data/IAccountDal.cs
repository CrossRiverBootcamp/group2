using Account.Data.Entities;

namespace Account.Data;

public interface IAccountDal
{
    Task<string> getCustomerByEmailAsync(string email);
    Task<int> SignInAsync(string email, string password);
    Task<Entities.Account> GetAccountInfoAsync(int id);
    Task<bool> SignUpAsync(Customer customer);
    Task TransferAmountAsync(int FromAccount, int toAccount, int amount);
    Task AddNewOperationAsync(Operation operationHistoryfrom, Operation operationHistoryfromTo);
    Task<List<OperationSecondSideModel>> GetOperationsByAccountIdAsync(int accountId, int position, int pageSize);
}

