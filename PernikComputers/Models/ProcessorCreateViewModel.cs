using System.ComponentModel.DataAnnotations;

namespace PernikComputers.Models
{
    public class ProcessorCreateViewModel
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
        [Display(Name = "CPU Speed")]
        [Range(1, int.MaxValue, ErrorMessage = "CPU Speed must be a positive number.")]
        public double CPUSpeed { get; set; }
        [Required]
        [Display(Name = "CPU Boost Speed")]
        [Range(1, int.MaxValue, ErrorMessage = "CPU Boost Speed must be a positive number.")]
        public double CPUBoostSpeed { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Cores must be a positive number.")]
        public int Cores { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Threads must be a positive number.")]
        public int Threads { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Cashe must be a positive number.")]
        public int Cache { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be a positive number.")]
        public int Quantity { get; set; }
        public string Image { get; set; }
    }
}
