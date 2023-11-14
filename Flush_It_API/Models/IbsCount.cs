using System.ComponentModel.DataAnnotations;

namespace Flush_It_API.Models
{
    public class IbsCount
    {
        [Key]
        public int Id { get; set; }
        public int Count { get; set; } = 0;
    }
}
