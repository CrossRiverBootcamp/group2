using Account.Data.Entities;

namespace Account.Data;

public interface IAccountDal
{
    Task<Customer> getCustomerByEmailAsync(string email);
    Task<int> SignInAsync(string email, string password);
    Task<Entities.Account> GetAccountInfoAsync(int id);
    Task SignUpAsync(Customer customer);
    Task TransferAmountAsync(Operation operationHistoryfrom, Operation operationHistoryfromTo, int amount);
    Task<List<OperationSecondSideModel>> GetOperationsByAccountIdAsync(int accountId, int currentPage, int pageSize);
    Task AddEmailVerificationAsync(EmailVerification emailVerification);
    Task<EmailVerification?> GetEmailVerificationAsync(string email);
    Task RemoveEmailVerificationAsync(EmailVerification verification);
    Task IncreaseNumOfTriesAsync(string email);
    Task<bool> EmailAddressExistsAsync(string email);
    Task<SendEmailTrack?> GetSendEmailTrackAsync(string email);
    Task AddSendEmailTrackAsync(SendEmailTrack set);
    Task UpdateSendEmailTrackAsync(SendEmailTrack set);
    Task RemoveSendEmailTrackAsync(string email);
}

