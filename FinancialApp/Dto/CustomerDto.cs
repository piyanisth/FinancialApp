namespace FinancialApp.Dto
{
    public class CustomerDto
    {
        public Guid UUID { get; set; }
        public string IDNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; } = string.Empty;
    }
}
