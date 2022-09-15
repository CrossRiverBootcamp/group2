using Account.Service.Models;

namespace Account.Service.Services
{
    public interface IEmailVerificationService
    {
        Task AddEmailVerificationAsync(string email);
        Task VerifyEmailAsync(EmailVerificationModel verification);
        Task RemoveEmailVerificationAsync(string email);
    }
}