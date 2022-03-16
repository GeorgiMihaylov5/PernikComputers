using PernikComputers.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace PernikComputers.Models
{
    public class VideoCardCreateViewModel
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
        public ChipManufacturer ChipManufacturer { get; set; }
        [Required]
        public string GraphicProcessor { get; set; }
        [Required]
        public int SizeMemory { get; set; }
        [Required]
        public string TypeMemory { get; set; }
        [Required]
        public int MemoryFrequency { get; set; }
        [Required]
        public int CoreFrequency { get; set; }
        [Required]
        public int CurrentProcesses { get; set; }
        [Required]
        public int RailWidth { get; set; }
        [Required]
        public string SlotType { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be a positive number.")]
        public int Quantity { get; set; }
        public string Image { get; set; }
    }
}
