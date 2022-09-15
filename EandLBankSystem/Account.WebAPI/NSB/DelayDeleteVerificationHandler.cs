using Account.Messages.Commands;
using Account.Service.Services;
using NServiceBus;

namespace Account.WebAPI.NSB;

public class DelayDeleteVerificationHandler : IHandleMessages<DelayDeleteVerification>
{
    private readonly IEmailVerificationService _emailVerificationService;

    public DelayDeleteVerificationHandler(IEmailVerificationService emailVerificationService)
    {
        _emailVerificationService = emailVerificationService;
    }

    public async Task Handle(DelayDeleteVerification message, IMessageHandlerContext context)
    {
        await Task.Delay(300000); //wait 5 minutes.
        await _emailVerificationService.RemoveEmailVerificationAsync(message.Email);
        await Task.CompletedTask;
    }
}
