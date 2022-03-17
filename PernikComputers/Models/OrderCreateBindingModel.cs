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
    }
}
