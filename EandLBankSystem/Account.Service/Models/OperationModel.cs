
namespace Account.Service.Models;

public class OperationModel
{
    public int SecondSideAccountId { get; set; }    
    public bool Credit { get; set; }
    public int TransactionAmount { get; set; }
    public int Balance { get; set; }
    public DateTime OperationTime { get; set; }

}

