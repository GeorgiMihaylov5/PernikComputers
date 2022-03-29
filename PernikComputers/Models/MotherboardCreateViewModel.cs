using PernikComputers.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace PernikComputers.Models
{
    public class MotherboardCreateViewModel
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
        [Required]
        public string Socket { get; set; }
        [Required]
        public string Chipset { get; set; }
        [Required]
        [Display(Name = "Type Ram")]
        public TypeRam TypeRam { get; set; }
        [Required]
        [Display(Name = "Number of memory slots")]
        public int RamSlotsCount { get; set; }
        [Required]
        [Display(Name = "Form Factor")]
        public string FormFactor { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be a positive number.")]
        public int Quantity { get; set; }
        public string Image { get; set; }
    }
}
