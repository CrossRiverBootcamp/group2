using Account.Service.Models;

namespace Account.Service
{
    public interface IOperationService
    {
        Task AddNewOperationsHistory(TransactionModel transactionModel);
        Task<List<OperationModel>> GetOperationsByAccountIdAsync(int accountId, int currentPage, int pageSize);
    }
}