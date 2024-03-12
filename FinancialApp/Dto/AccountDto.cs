namespace FinancialApp.Dto
{
    public class AccountDto
    {
        public Guid UUID { get; set; }
        public string Currency { get; set; }
        public string AccountName { get; set; }
        public CustomerDto Customer { get; set; }
    }
}
