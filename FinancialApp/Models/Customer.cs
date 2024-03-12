using System.Security.Principal;
using FinancialApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations;

namespace FinancialApp.Models
{
    public class Customer
    {
        [Key]
        public Guid UUID { get; set; }
        public string IDNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        // Navigation property for related accounts
        public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
    }
}

