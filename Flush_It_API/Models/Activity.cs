using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flush_It_API.Models
{
    public class Activity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public bool HadAttack { get; set; } = false;

        public bool IsHealthyDay { get; set; } = true;

        [ForeignKey("UserId")]
        public int UserId { get; set; }

        public User? User { get; set; }  // Navigation property to User

        public List<FoodActivity> FoodActivities { get; set; }
    }
}
