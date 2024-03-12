namespace FinancialApp.Dto
{
    public class AccountCreationDto
    {
        public Guid CustomerId { get; set; }
        public string Currency { get; set; }
        public string AccountName { get; set; } = string.Empty;
    }
}
