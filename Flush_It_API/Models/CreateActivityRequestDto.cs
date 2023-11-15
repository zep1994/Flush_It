namespace Flush_It_API.Models
{
    public class CreateActivityRequestDto
    {
        public DateTime Date { get; set; }
        public bool IsHealthyDay { get; set; }
        public List<string> FoodNames { get; set; }
        public bool HadAttack { get; set; }
    }
}
