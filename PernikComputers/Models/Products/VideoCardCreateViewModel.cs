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
        [Required]
        [Display(Name = "Chip Manufacturer")]
        public ChipManufacturer ChipManufacturer { get; set; }
        [Required]
        [Display(Name = "Graphic Processor")]
        public string GraphicProcessor { get; set; }
        [Required]
        [Display(Name = "Memory Capacity")]
        public int SizeMemory { get; set; }
        [Required]
        [Display(Name = "Type Memory")]
        public string TypeMemory { get; set; }
        [Display(Name = "Memory Frequency")]
        [Required]
        public int MemoryFrequency { get; set; }
        [Required]
        [Display(Name = "Core Frequency")]
        public int CoreFrequency { get; set; }
        [Required]
        [Display(Name = "Current Processes")]
        public int CurrentProcesses { get; set; }
        [Required]
        [Display(Name = "Rail Width")]
        public int RailWidth { get; set; }
        [Required]
        [Display(Name = "Slot Type")]
        public string SlotType { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be a positive number.")]
        public int Quantity { get; set; }
        public string Image { get; set; }
    }
}
