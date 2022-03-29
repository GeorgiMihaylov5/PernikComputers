using System.ComponentModel.DataAnnotations;

namespace PernikComputers.Models
{
    public class PowerSupplyCreateViewModel
    {
        public string Id { get; set; }
        [Required]
        public string Barcode { get; set; }
        [Required]
        public string Manufacturer { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public int Warranty { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public decimal Price { get; set; }
        public int Promotion { get; set; }
        [Required]
        public int Power { get; set; }
        [Required]
        [Display(Name = "Form Factor")]
        public string FormFactor { get; set; }
        [Required]
        [Range(1,100)]
        public int Efficiency { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be a positive number.")]
        public int Quantity { get; set; }
        public string Image { get; set; }
    }
}
