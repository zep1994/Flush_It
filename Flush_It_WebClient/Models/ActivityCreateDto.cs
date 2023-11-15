namespace Flush_It_WebClient.Models
{
    public class ActivityCreateDto
    {
        public DateTime Date { get; set; }

        public bool HadAttack { get; set; }

        public bool IsHealthyDay { get; set; }

        public List<string>? FoodNames { get; set; }
        public int UserId { get; set; }
    }
}
