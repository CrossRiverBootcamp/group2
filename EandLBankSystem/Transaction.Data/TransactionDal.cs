using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction.Data.Entities;

namespace Transaction.Data;

public class TransactionDal : ITransactionDal
{
    private IDbContextFactory<TransactionContext> _factory;

    public TransactionDal(IDbContextFactory<TransactionContext> factory)
    {
        _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        using var db = _factory.CreateDbContext();
        db.Database.Migrate();
    }

    public async Task<int> PostTransactionAsync(Entities.Transaction transaction)
    {
        using var db = _factory.CreateDbContext();
        await db.Transactions.AddAsync(transaction);
        await db.SaveChangesAsync();
        return transaction.Id;
    }

    public async Task UpdateTransactionStatusAsync(int transactionId, bool success, string? failureMessage)
    {
        using var db = _factory.CreateDbContext();
        var transaction = await db.Transactions.FirstOrDefaultAsync(t => t.Id == transactionId) ?? throw new Exception("Transaction doen't exist.");
        transaction.Status =  success switch { true => EStatus.Success, false=> EStatus.Fail };
        transaction.FailureReason = failureMessage;
        await db.SaveChangesAsync();
    }

}
