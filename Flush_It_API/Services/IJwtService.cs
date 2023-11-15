using System.Security.Claims;

namespace Flush_It_API.Services
{
    public interface IJwtService
    {
        string GenerateToken(int userId, string username);
        ClaimsPrincipal ValidateToken(string token);
    }
}
