using Flush_It_API.Data;
using Flush_It_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

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


        #region CRUD
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

        #endregion

        #region Search

        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetFoodByName(string name)
        {
            var food = await _context.Food
                .FirstOrDefaultAsync(f => EF.Functions.ILike(f.Name, "%" + name + "%"));

            if (food == null)
            {
                return NotFound($"Food item with name '{name}' not found.");
            }

            return CheckFood(food);
        }

        [HttpGet("check")]
        public IActionResult CheckFood([FromBody] Food fooditem)
        {
            if (fooditem == null)
            {
                return BadRequest("Food item not provided.");
            }

            var food = _context.Food.Find(fooditem.Id);

            if (food == null)
            {
                return NotFound("Food item not found.");
            }

            var response = new StringBuilder();

            switch (true)
            {
                // Check TotalFat
                case var fatCondition when food.TotalFat.HasValue && food.TotalFat > 20:
                    response.AppendLine("This food item contains greater than 20 of Total Fat, so we recommend staying away for now.");
                    break;

                // Example: Check if the food is spicy
                case var spicyCondition when food.Spicy.HasValue && food.Spicy == true:
                    response.AppendLine("This food item is spicy.");
                    break;

                // Example: Check if the food is fried
                case var friedCondition when food.Fried.HasValue && food.Fried == true:
                    response.AppendLine("This food item is fried.");
                    break;

                // Example: Check if the food is Bread
                case var breadCondition when food.Bread.HasValue && food.Bread == true:
                    response.AppendLine("This food item contains bread.");
                    break;

                // Example: Check if the food is fried
                case var glutenCondition when food.Gluten.HasValue && food.Gluten == true:
                    response.AppendLine("This food contains Gluten.");
                    break;

                // Example: Check if the food is Fiber
                case var fiberCondition when food.Fiber.HasValue && food.Fiber > 2:
                    response.AppendLine("This food contains fiber and passed the other test, so you should be good to eat this.");
                    break;

                // If none of the conditions is met, return a generic response
                default:
                    response.AppendLine("Nothing Special to see here. Perhaps try to find a more nutrient dense food?");
                    break;
            }
                    return Ok(response.ToString());
        }
        #endregion
    }
}
