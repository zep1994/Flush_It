using Flush_It_API.Data;
using Flush_It_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Flush_It_API.Controllers
{
    [Route("api/[controller]/")]
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

        [HttpPut("{id}")]
        public IActionResult Put(int id, Food food)
        {
            var existingItem = _context.Food.FirstOrDefault(item => item.Id == id);

            if (existingItem == null)
            {
                return NotFound();
            }

            // Use reflection to get all public properties of the Food model
            var properties = typeof(Food).GetProperties();

            foreach (var property in properties)
            {
                // Exclude the Id property from the update process
                if (property.DeclaringType == typeof(Food) && property.Name != "Id")
                {
                    // Get the value from the incoming foodItem and set it on the existing item
                    var value = property.GetValue(food);
                    property.SetValue(existingItem, value);
                }
            }

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existingItem = _context.Food.FirstOrDefault(item => item.Id == id);
            if (existingItem == null)
            {
                return NotFound();
            }

            _context.Food.Remove(existingItem);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
