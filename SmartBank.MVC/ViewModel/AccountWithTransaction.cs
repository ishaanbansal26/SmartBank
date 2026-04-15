using SmartBank.MVC.Services;

namespace SmartBank.MVC.ViewModel
{
    public class AccountWithTransaction
    {
        public AccountDto Account { get; set; }
        public List<TransactionDto> Transactions { get; set; } = new List<TransactionDto>();
    }

}
