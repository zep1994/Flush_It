using System.Security.Claims;

namespace Flush_It_API.Services
{
    public interface IJwtService
    {
        string GenerateToken(string userId, string username);
        ClaimsPrincipal ValidateToken(string token);
    }
}
