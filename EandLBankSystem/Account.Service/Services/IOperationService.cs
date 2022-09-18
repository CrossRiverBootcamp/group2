using Account.Data;
using Account.Service.Models;

namespace Account.Service
{
    public interface IOperationService
    {
        Task<List<OperationModel>> GetOperationsByAccountIdAsync(int accountId, int currentPage, int pageSize);
    }
}