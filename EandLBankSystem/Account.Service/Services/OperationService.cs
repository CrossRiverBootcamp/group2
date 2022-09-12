using Account.Data;
using Account.Data.Entities;
using Account.Service.Models;
using AutoMapper;

namespace Account.Service;

public class OperationService : IOperationService
{
    private readonly IAccountDal _accountDal;
    private readonly IMapper _mapper;
    public OperationService(IAccountDal accountDal)
    {
        _accountDal = accountDal;
        _mapper = ConfigureAutoMapper();
    }

    public async Task AddNewOperationsHistory(TransactionModel transactionModel)
    {
        Data.Entities.Account fromAccount = await _accountDal.GetAccountInfoAsync(transactionModel.FromAccount);
        Operation fromOperationHistory = new()
        {
            AccountId = transactionModel.FromAccount,
            TransactionId = transactionModel.TransactionId,
            Credit = false,
            TransactionAmount = transactionModel.Amount,
            Balance = fromAccount.Balance,
            OperationTime = DateTime.UtcNow
        };

        Data.Entities.Account toAccount = await _accountDal.GetAccountInfoAsync(transactionModel.ToAccount);
        Operation toOperationHistory = new()
        {
            AccountId = transactionModel.ToAccount,
            TransactionId = transactionModel.TransactionId,
            Credit = true,
            TransactionAmount = transactionModel.Amount,
            Balance = toAccount.Balance,
            OperationTime = DateTime.UtcNow
        };

        await _accountDal.AddNewOperationAsync(fromOperationHistory, toOperationHistory);
    }
    public async Task<List<OperationModel>> GetOperationsByAccountIdAsync(int accountId, int position, int pageSize)
    {
        List<Operation> l = await _accountDal.GetOperationsByAccountIdAsync(accountId, position, pageSize);
        return _mapper.Map<List<OperationModel>>(l);
    }
    private static IMapper ConfigureAutoMapper()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ServiceAutoMapper>();
        });
        return config.CreateMapper();
    }
}

