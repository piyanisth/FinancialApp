using FinancialApp.Data;
using FinancialApp.Dto;
using FinancialApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinancialApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly MyDatabaseContext _context;

        public CustomersController(MyDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomers()
        {
            // returns only the selected columns for all customers
            return await _context.Customers
                .Select(c => new CustomerDto
                {
                    UUID = c.UUID,
                    IDNumber = c.IDNumber,
                    Name = c.Name,
                    Surname = c.Surname
                }).ToListAsync();
        }

        // GET: api/Customers/{uuid}
        [HttpGet("{uuid}")]
        public async Task<ActionResult<CustomerDto>> GetCustomer(Guid uuid)
        {
            var customerDto = await _context.Customers
                .Where(c => c.UUID == uuid)
                .Select(c => new CustomerDto
                {
                    UUID = c.UUID,
                    IDNumber = c.IDNumber,
                    Name = c.Name,
                    Surname = c.Surname
                }).FirstOrDefaultAsync();

            if (customerDto == null)
            {
                return NotFound();
            }

            return customerDto;
        }

        // POST: api/Customers
        [HttpPost]
        public async Task<ActionResult<CustomerDto>> PostCustomer(CustomerDto customerDto)
        {
            var customer = new Customer
            {
                UUID = Guid.NewGuid(),
                IDNumber = customerDto.IDNumber,
                Name = customerDto.Name,
                Surname = customerDto.Surname
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            // Return the DTO including the newly generated UUID
            customerDto.UUID = customer.UUID;

            return CreatedAtAction("GetCustomer", new { uuid = customer.UUID }, customerDto);
        }

        // PUT: api/Customers/{uuid}
        [HttpPut("{uuid}")]
        public async Task<IActionResult> PutCustomer(Guid uuid, CustomerDto customerDto)
        {
            if (uuid != customerDto.UUID)
            {
                return BadRequest();
            }

            var customer = await _context.Customers.FindAsync(uuid);
            if (customer == null)
            {
                return NotFound();
            }

            customer.IDNumber = customerDto.IDNumber;
            customer.Name = customerDto.Name;
            customer.Surname = customerDto.Surname;

            _context.Entry(customer).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Customers/{uuid}
        [HttpDelete("{uuid}")]
        public async Task<IActionResult> DeleteCustomer(Guid uuid)
        {
            var customer = await _context.Customers.FindAsync(uuid);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
