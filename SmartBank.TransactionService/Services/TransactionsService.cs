using SmartBank.TransactionService.DTOs;
using SmartBank.TransactionService.Models;
using SmartBank.TransactionService.Repositories;

namespace SmartBank.TransactionService.Services
{
    public class TransactionsService : ITransactionService
    {
        private ITransactionRepository _repository;

        public TransactionsService(ITransactionRepository repository)
        {
            _repository = repository;
        }
        public async Task<Transaction> CreateTransaction(CreateTransactionDto createTransactionDto)
        {
            if (string.IsNullOrEmpty(createTransactionDto.Type) || createTransactionDto.Amount < 0)
                throw new Exception();
            var x = new Transaction
            {
                AccountId = createTransactionDto.AccountId,
                Type = createTransactionDto.Type,
                Amount = createTransactionDto.Amount,
                //Description = createTransactionDto.Description,
                Date = DateTime.Now
            };
            //x.Date = DateTime.Now;
            await _repository.CreateTransaction(x);
            return x;
        }

        //public async Task<Transaction> GetTransactionbyId(int id)
        //{
        //    var x = await _repository.GetTransactionById(id);
        //    if (x == null)
        //        throw new Exception("Not Found");
        //    return x;
        //}

        public async Task<IEnumerable<Transaction>> GetTransactions(int accountId)
        {
            return await _repository.GetTransactions(accountId);
        }
    }
}
