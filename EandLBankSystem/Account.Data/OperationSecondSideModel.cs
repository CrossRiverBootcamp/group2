
namespace Account.Data;

public class OperationSecondSideModel
{
    
    public int Id { get; set; }
  
    public int SecondSideAccountId { get; set; }
    
    public int TransactionId { get; set; }
 
    public bool Credit { get; set; }
  
    public int TransactionAmount { get; set; }
  
    public int Balance { get; set; }
   
    public DateTime OperationTime { get; set; }
}

