namespace SmartBank.TransactionService.DTOs
{
    public class CreateTransactionDto
    {
        public int AccountId { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; } 
    }
}
