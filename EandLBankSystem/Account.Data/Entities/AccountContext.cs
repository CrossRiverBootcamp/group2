using Microsoft.EntityFrameworkCore;

namespace Account.Data.Entities;

public class AccountContext : DbContext
{
    public AccountContext(DbContextOptions<AccountContext> options) : base(options) { }
    
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Operation> Operations { get; set; }
    public DbSet<EmailVerification> EmailVerifications { get; set; }
    public DbSet<SendEmailTrack> SendEmailTracks { get; set; }
}



