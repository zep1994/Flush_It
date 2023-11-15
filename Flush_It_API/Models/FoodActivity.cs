namespace Flush_It_API.Models
{
    public class FoodActivity
    {
        public int FoodId { get; set; }
        public Food Food { get; set; }

        public int ActivityId { get; set; }
        public Activity Activity { get; set; }
    }
}
