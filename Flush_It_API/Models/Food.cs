using System.ComponentModel.DataAnnotations;

namespace Flush_It_API.Models
{
    public class Food
    {
        //Core Part
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? Calories { get; set; } = 0;
        public double? Sugar { get; set; } = 0;
        public double? Sodium { get; set; } = 0;
        public double? Potassium { get; set; } = 0;
        public double? Calcium {  get; set; } = 0;
        public double? Iron { get; set; } = 0;
        public double? Carbs { get; set; } = 0;
        public double? Protein { get; set; } = 0;
        public double? TotalFat { get; set; } = 0;
        public double? SaturatedFat { get; set; } = 0;
        public double? TransFat { get; set; } = 0;
        public double? Cholesterol { get; set; } = 0;
        public double? Fiber { get; set; } = 0;

        //Dietary Suggestions
        public bool? Alcohol { get; set; } = false;
        public bool? Processed { get; set; } = false;
        public bool? FODMAP { get; set; } = false;
        public bool? DairyProduct { get; set; } = false;
        public bool? Gluten { get; set; } = false;
        public bool? Spicy { get; set; } = false;
        public bool? Fried { get; set; } = false;
        public bool? Caffeine { get; set; } = false;
        public bool? Sweetener { get; set; } = false;
        public bool? Bread { get; set; } = false;
        public bool? ArtificialAdditives { get; set; } = false;
        public bool? CarbonatedBeverage { get; set; } = false;
    }
}
