using System.Transactions;
using Microsoft.EntityFrameworkCore;
using SmartBank.TransactionService.Data;

namespace SmartBank.TransactionService.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private SmartBankTransactionServiceContext _context;

        public TransactionRepository(SmartBankTransactionServiceContext context)
        {
            _context = context;
        }

        
        public async Task CreateTransaction(Models.Transaction transaction)
        {
            await _context.Transaction.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Models.Transaction>> GetTransactions(int accountId)
        {
            return await _context.Transaction.Where(x=>x.AccountId==accountId).ToListAsync();
        }

        //public async Task<Models.Transaction> GetTransactionById(int id)
        //{
        //    return await _context.Transaction.FindAsync(id);
        //}
    }
}
