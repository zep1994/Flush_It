using System.ComponentModel.DataAnnotations;

namespace Flush_It_WebClient.Models
{
    public class UserLoginDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
