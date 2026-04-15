using SmartBank.TransactionService.Models;

namespace SmartBank.TransactionService.Repositories
{
    public interface ITransactionRepository
    {
        Task CreateTransaction(Transaction transaction);
        Task<IEnumerable<Transaction>> GetTransactions(int accountId);
        //Task<Transaction> GetTransactionById(int id);
    }
}
