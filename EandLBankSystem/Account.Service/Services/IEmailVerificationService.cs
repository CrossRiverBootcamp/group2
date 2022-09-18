using Account.Service.Models;

namespace Account.Service.Services
{
    public interface IEmailVerificationService
    {
        Task VerifyEmailAsync(EmailVerificationModel verification);
        Task RemoveEmailVerificationAsync(string email);
        Task AddEmailVerificationProcAsync(string email);
    }
}