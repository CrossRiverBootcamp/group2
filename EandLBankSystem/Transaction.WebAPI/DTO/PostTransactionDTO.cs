namespace Transaction.WebAPI.DTO
{
    public class PostTransactionDTO
    {
        public int FromAccount { get; set; }
        public int ToAccount { get; set; }
        public int Ammount { get; set; }
    }
}
