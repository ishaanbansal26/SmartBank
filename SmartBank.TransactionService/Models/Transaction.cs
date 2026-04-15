namespace SmartBank.TransactionService.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; } // Deposit / Withdraw
        //public string? Description { get; set; }
        public DateTime Date { get; set; }
    }
}
