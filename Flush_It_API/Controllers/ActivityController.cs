using Flush_It_API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Flush_It_API.Models;
using System.IdentityModel.Tokens.Jwt;
using Flush_It_WebClient.Models;
using Microsoft.AspNetCore.Authorization;

namespace Flush_It_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ActivityController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ActivityController(AppDbContext context, ILogger<ActivityController> logger)
        {
            _context = context;
        }


        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateActivity([FromBody] ActivityCreateDto requestDto)
        {
            try
            {
                // Get the JWT token from the request headers
                var authorizationHeader = HttpContext.Request.Headers["Authorization"].ToString();
                var token = authorizationHeader.StartsWith("Bearer ") ? authorizationHeader.Substring(7) : "";


                // Check if the token is null or empty
                if (string.IsNullOrEmpty(token))
                {
                    return Unauthorized(new { message = "Invalid token. Token is null or empty." });
                }

                // Decode and inspect the token
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

                // Log the decoded token claims
                foreach (var claim in jsonToken.Claims)
                {
                    Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");
                }

                var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

                var userId = userIdClaim?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized(new { message = "Invalid token. User not authenticated." });
                }

                var foods = await _context.Food
                    .Where(food => requestDto.FoodNames.Contains(food.Name))
                    .ToListAsync();

                var activity = new Activity
                {
                    Date = requestDto.Date,
                    IsHealthyDay = requestDto.IsHealthyDay,
                    HadAttack = requestDto.HadAttack,
                    UserId = int.Parse(userId),
                    FoodActivities = foods.Select(food => new FoodActivity { Food = food }).ToList()
                };

                _context.Activities.Add(activity);
                await _context.SaveChangesAsync();

                return Ok("Activity created successfully!");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
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
