using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStoreDBFirst.Models;

namespace BookStoreDBFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DealerController : ControllerBase
    {
        private readonly FruitDealerDbContext _context;

        public DealerController(FruitDealerDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dealer>>> GetAllDealers()
        {
            var dealers = await _context.Dealers.ToListAsync();
            return Ok(dealers);
        }

        [HttpPost]
        public async Task<ActionResult> AddDealer(Dealer dealer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return detailed validation errors
            }
            await _context.Dealers.AddAsync(dealer);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDealer(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid Dealer id");

            var dealer = await _context.Dealers.FindAsync(id);
              _context.Dealers.Remove(dealer);
                await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
