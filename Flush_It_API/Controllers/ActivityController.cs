using Flush_It_API.Data;
using Flush_It_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Flush_It_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActivityController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ActivityController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateActivity([FromBody] CreateActivityRequestDto requestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var foods = await _context.Food
                .Where(food => requestDto.FoodNames.Contains(food.Name))
                .ToListAsync();

            var activity = new Activity
            {
                Date = requestDto.Date,
                IsHealthyDay = requestDto.IsHealthyDay,
                HadAttack = requestDto.HadAttack,
                FoodActivities = foods.Select(food => new FoodActivity { Food = food }).ToList()
            };

            _context.Activities.Add(activity);
            await _context.SaveChangesAsync();

            return Ok("Activity created successfully!");
        }

        [HttpGet("{activityId}")]
        public IActionResult GetActivityDetails(int activityId)
        {
            var activity = _context.Activities
                .Include(a => a.FoodActivities)
                .ThenInclude(fa => fa.Food)
                .SingleOrDefault(a => a.Id == activityId);

            if (activity == null)
            {
                return NotFound($"Activity with ID {activityId} not found");
            }

            var activityDetails = new
            {
                activity.Id,
                activity.Date,
                activity.IsHealthyDay,
                activity.HadAttack,
                Foods = activity.FoodActivities
                    .Select(fa => new
                    {
                        fa.Food.Id,
                        fa.Food.Name,
                        fa.Food.Description,
                        fa.Food.Calories,
                        fa.Food.Sugar,
                        fa.Food.Sodium,
                    })
            };

            return Ok(activityDetails);
        }
    }
}
