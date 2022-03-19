using PernikComputers.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace PernikComputers.Models
{
    public class MemoryCreateViewModel
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
        public bool IsPromotion { get; set; }
        [Required]
        [Display(Name = "Memory Type")]
        public MemoryType MemoryType { get; set; }
        [Required]
        [Display(Name = "Form Factor")]
        public string FormFactor { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Capacity must be a positive number.")]
        public int Capacity { get; set; }
        [Required]
        [Display(Name = "Reading Speed")]
        public int ReadSpeed { get; set; }
        [Required]
        [Display(Name = "Recording Speed")]
        public int WriteSpeed { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be a positive number.")]
        public int Quantity { get; set; }
        public string Image { get; set; }
    }
}
