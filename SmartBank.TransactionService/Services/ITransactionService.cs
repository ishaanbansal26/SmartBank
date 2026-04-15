using SmartBank.TransactionService.DTOs;
using SmartBank.TransactionService.Models;

namespace SmartBank.TransactionService.Services
{
    public interface ITransactionService
    {
        Task<Transaction> CreateTransaction(CreateTransactionDto createTransactionDto);
        //Task<Transaction> GetTransactionbyId(int id);
        Task<IEnumerable<Transaction>> GetTransactions(int accountId);

    }
}
