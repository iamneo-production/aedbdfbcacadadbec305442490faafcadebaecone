using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStoreDBFirst.Models;

namespace BookStoreDBFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FruitController : ControllerBase
    {
        private readonly FruitDealerDbContext _context;

        public FruitController(FruitDealerDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fruit>>> GetAllFruits()
        {
            var fruits = await _context.Fruits.ToListAsync();
            return Ok(fruits);
        }
// [HttpGet("FruitName")]
// public async Task<ActionResult<IEnumerable<string>>> Get()
// {
//     // Project the JobTitle property using Select
//     var fruitNames = await _context.Fruits
//         .OrderBy(x => x.FruitName)
//         .Select(x => x.FruitName)
//         .ToListAsync();

//     return fruitNames;
// }
        [HttpPost]
        public async Task<ActionResult> AddFruit(Fruit fruit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return detailed validation errors
            }
            await _context.Fruits.AddAsync(fruit);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFruit(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid Fruit id");

            var fruit = await _context.Fruits.FindAsync(id);
              _context.Fruits.Remove(fruit);
                await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
