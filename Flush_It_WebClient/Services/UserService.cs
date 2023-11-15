using System.Security.Claims;

namespace Flush_It_WebClient.Services
{
    // Example: Assuming you have a service to manage user-related information
    public class UserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetCurrentUserId()
        {
            // Get the user's ID from the claims
            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out var userId))
            {
                return userId;
            }

            // Handle the case where the user ID is not available
            return 0;
        }
    }
}