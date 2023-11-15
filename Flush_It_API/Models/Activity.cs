using System.ComponentModel.DataAnnotations;

namespace Flush_It_API.Models
{
    public class Activity
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date {  get; set; }
        public List<Food>? Foods { get; set; }
        public bool HadAttack { get; set; } = false;
        public bool IsHealthyDay { get; set; } = true;
        public List<FoodActivity> FoodActivities { get; set; }
        public int UserId { get; internal set; }
    }
}
