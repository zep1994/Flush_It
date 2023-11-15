namespace Flush_It_API.Models
{
    public class JwtSettings
    {
        public string Secret { get; set; }
        public int ExpireMinutes { get; set; }
    }
}
