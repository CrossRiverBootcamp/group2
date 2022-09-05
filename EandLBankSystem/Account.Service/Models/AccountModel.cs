using Account.Data.Entities;

namespace Account.Service.Models;

public class AccountModel
{
    public DateTime OpenDate { get; set; }
    public decimal Balance { get; set; }
    public Customer? Customer { get; set; }
}

