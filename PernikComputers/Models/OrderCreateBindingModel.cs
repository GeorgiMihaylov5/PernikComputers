using PernikComputers.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace PernikComputers.Models
{
    public class OrderCreateBindingModel
    {
        [Required]
        public string ProductId { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        [Display(Name = "Products")]
        public int Count { get; set; }
        public Category Category { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string Price { get; set; }
    }
}
