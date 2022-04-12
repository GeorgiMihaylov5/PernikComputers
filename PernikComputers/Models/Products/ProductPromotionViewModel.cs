using System.ComponentModel.DataAnnotations;

namespace PernikComputers.Models
{
    public class ProductPromotionViewModel
    {
        public string Id { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public decimal OldPrice { get; set; }
        public decimal NewPrice { get; set; }
        [Display(Name = "Add Promotion")]
        [Range(0, 100)]
        public decimal Discount { get; set; }
        public string Image { get; set; }
    }
}
