using FinancialApp.Data;
using FinancialApp.Dto;
using FinancialApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace FinancialApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly MyDatabaseContext _context;

        public AccountsController(MyDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Accounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountDto>>> GetAccounts()
        {
            var accounts = await _context.Accounts
                .Include(a => a.Customer)
                .Select(a => new AccountDto
                {
                    UUID = a.UUID,
                    Currency = a.Currency,
                    AccountName = a.AccountName,
                    Customer = new CustomerDto
                    {
                        UUID = a.Customer.UUID,
                        IDNumber = a.Customer.IDNumber,
                        Name = a.Customer.Name,
                        Surname = a.Customer.Surname
                    }
                }).ToListAsync();

            return Ok(accounts);
        }

        // POST: api/Accounts
        [HttpPost]
        public async Task<ActionResult<AccountDto>> PostAccount([FromBody] AccountCreationDto accountDto)
        {
            var customer = await _context.Customers.FindAsync(accountDto.CustomerId);
            if (customer == null)
            {
                return NotFound($"Customer with ID {accountDto.CustomerId} not found.");
            }

            var account = new Account
            {
                UUID = Guid.NewGuid(),
                CustomerId = accountDto.CustomerId,
                Currency = accountDto.Currency,
                AccountName = accountDto.AccountName
            };

            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            var result = new AccountDto
            {
                UUID = account.UUID,
                Currency = account.Currency,
                AccountName = account.AccountName,
                Customer = new CustomerDto
                {
                    UUID = customer.UUID,
                    IDNumber = customer.IDNumber,
                    Name = customer.Name,
                    Surname = customer.Surname
                }
            };

            return CreatedAtAction(nameof(GetAccount), new { uuid = account.UUID }, result);
        }

        // GET: api/Accounts/{uuid}
        [HttpGet("{uuid}")]
        public async Task<ActionResult<AccountDto>> GetAccount(Guid uuid)
        {
            var account = await _context.Accounts
                .Include(a => a.Customer)
                .Select(a => new AccountDto
                {
                    UUID = a.UUID,
                    Currency = a.Currency,
                    AccountName = a.AccountName,
                    Customer = new CustomerDto
                    {
                        UUID = a.Customer.UUID,
                        IDNumber = a.Customer.IDNumber,
                        Name = a.Customer.Name,
                        Surname = a.Customer.Surname
                    }
                })
                .FirstOrDefaultAsync(a => a.UUID == uuid);

            if (account == null)
            {
                return NotFound();
            }

            return account;
        }

        // PUT: api/Accounts/{uuid}
        [HttpPut("{uuid}")]
        public async Task<IActionResult> PutAccount(Guid uuid, AccountUpdateDto accountUpdateDto)
        {
            var account = await _context.Accounts.FindAsync(uuid);
            if (account == null)
            {
                return NotFound();
            }

            if (accountUpdateDto.Currency != null)
            {
                account.Currency = accountUpdateDto.Currency;
            }

            if (accountUpdateDto.AccountName != null)
            {
                account.AccountName = accountUpdateDto.AccountName;
            }

            _context.Entry(account).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Accounts.Any(e => e.UUID == uuid))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Accounts/{uuid}
        [HttpDelete("{uuid}")]
        public async Task<IActionResult> DeleteAccount(Guid uuid)
        {
            var account = await _context.Accounts.FindAsync(uuid);
            if (account == null)
            {
                return NotFound();
            }

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
