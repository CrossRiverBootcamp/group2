namespace Account.Service.Models;

public class EmailVerificationModel
{
    public string Email { get; set; }
    public string? Code { get; set; }
}
