using System.ComponentModel.DataAnnotations;

namespace FinancialApp.Models
{
    public class Account
    {
        [Key]
        public Guid UUID { get; set; }
        public Guid CustomerId { get; set; }
        public string Currency { get; set; }
        public string AccountName { get; set; }
        
        // Navigation property for the related customer
        public virtual Customer Customer { get; set; }
    }
}
