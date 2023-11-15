using Flush_It_API.Data;
using Flush_It_API.Models;
using Flush_It_API.Services;
using Flush_It_API.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text;

namespace Flush_It_API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IBCryptHasher _bcryptHasher;
        private readonly IJwtService _jwtService;

        public AuthController(AppDbContext context, IBCryptHasher bcryptHasher, IJwtService jwtService)
        {
            _context = context;
            _bcryptHasher = bcryptHasher;
            _jwtService = jwtService;

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                // Check if username or email already exists
                if (await _context.Users.AnyAsync(u => u.Username == request.Username || u.Email == request.Email))
                {
                    throw new Exception("Username or email already exists.");
                }

                // Hash the password
                string passwordHash = _bcryptHasher.HashPassword(request.Password);

                // Create user
                var user = new User
                {
                    Username = request.Username,
                    Email = request.Email,
                    PasswordHash = passwordHash
                };

                // Save to database
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                // Generate JWT token
                var token = _jwtService.GenerateToken(user.Id.ToString(), user.Username);

                // Return token in response
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                // Find the user by username
                var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == request.Username);

                // Check if the user exists
                if (user == null)
                {
                    return BadRequest(new { message = "Invalid credentials." });
                }

                // Verify the password
                if (!_bcryptHasher.VerifyPassword(request.Password, user.PasswordHash))
                {
                    return BadRequest(new { message = "Invalid credentials." });
                }

                // Generate JWT token
                var token = _jwtService.GenerateToken(user.Id.ToString(), user.Username);

                // Return token in response
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }       
    }
}