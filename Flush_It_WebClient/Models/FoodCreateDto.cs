using System.ComponentModel.DataAnnotations;

namespace Flush_It_WebClient.Models
{
    public class FoodCreateDto
    {
        [Required]
        public string Name { get; set; }
    }
}
