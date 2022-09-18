using Account.Data;
using Account.Messages.Commands;
using Account.Service.Models;
using Microsoft.Extensions.Options;
using NServiceBus;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;

namespace Account.Service.Services;

public class EmailVerificationService : IEmailVerificationService
{
    private readonly IAccountDal _accountDal;
    private readonly IMessageSession _messageSession;
    private readonly MailSettings _mailSettings;

    public EmailVerificationService(IAccountDal accountDal, IOptions<MailSettings> mailSettings, IMessageSession messageSession)
    {
        _accountDal = accountDal;
        _messageSession = messageSession;
        _mailSettings = mailSettings.Value;
    }

    public async Task AddEmailVerificationAsync(string email)
    {
        
        var code = Convert.ToHexString(RandomNumberGenerator.GetBytes(2));
        if (await _accountDal.EmailAddressExistsAsync(email))
            throw new ArgumentException("Email already exists", email);

        List <Task> tasks = new();
        tasks.Add(SendVerificationEmailAsync(email, code));
        tasks.Add(AddEmailRecordAsync(email, code));

        var sendOptions = new SendOptions();
        sendOptions.DelayDeliveryWith(TimeSpan.FromMinutes(5));
        sendOptions.RouteToThisEndpoint();

        tasks.Add(_messageSession.Send(new DelayDeleteVerification() { Email = email }, sendOptions));
        await Task.WhenAll(tasks);
    }
    private async Task AddEmailRecordAsync(string email, string code)
    {
        await RemoveEmailVerificationAsync(email);
        await _accountDal.AddEmailVerificationAsync(new() { Code = code, Email = email });
    }
    public async Task VerifyEmailAsync(EmailVerificationModel verification)
    {
        var relevantVerification = await _accountDal.GetEmailVerificationAsync(verification.Email);

        if (relevantVerification == null)
            throw new InvalidOperationException("No verification is availble for the given email address.");
        if (relevantVerification.NumOfTries >= 5)
            throw new InvalidOperationException("Too many attempts to resource.");
        if (relevantVerification.ExpirationTime >= DateTime.UtcNow)
            throw new InvalidOperationException("Action has expired.");
        if (relevantVerification.Code != verification.Code)
        {
            await _accountDal.IncreaseNumOfTriesAsync(relevantVerification.Email);
            throw new InvalidOperationException("Wrong verification code.");
        }
       
    }
    public async Task RemoveEmailVerificationAsync(string email)
    {
        var relevantVerification = await _accountDal.GetEmailVerificationAsync(email);
        if (relevantVerification != null)
            await _accountDal.RemoveEmailVerificationAsync(relevantVerification);
    }
    private async Task SendVerificationEmailAsync(string toEmail, string code)
    {
        var client = new SmtpClient(_mailSettings.Host, _mailSettings.Port)
        {
            Credentials = new NetworkCredential(_mailSettings.Mail, _mailSettings.Password),
            EnableSsl = true
        };

        MailMessage mail = new MailMessage()
        {
            Subject = "Verification email [E & L Bank]",
            From = new MailAddress(_mailSettings.Mail),
            Body = "Hello! <br/> Thank you for joining to E&L banking system! <br/> Your private verification code is: <br/><br/><h3>[  " + code + "  ]</h3> <br/><i>*this code will be active for 5 minutes.</i> <br/> See you soon!",
            IsBodyHtml = true,
        };
        mail.To.Add(toEmail);

        await client.SendMailAsync(mail);
    }
}
