using Flush_It_API.Data;
using Flush_It_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Flush_It_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FoodController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult GetFoodItems()
        {
            return Ok(_context.Food.ToList());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFoodItem(int id)
        {
            var food = await _context.Food.FindAsync(id);
            if (food == null)
            {
                return NotFound();
            }
            return Ok(food);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFoodItem([FromBody] Food food)
        {
            if (food == null) return BadRequest();

            _context.Food.Add(food);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFoodItem), new { id = food.Id }, food);
        }
    }
}
