using System.ComponentModel.DataAnnotations;

namespace PernikComputers.Models
{
    public class ComputerCaseCreateViewModel
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
        [Display(Name = "Case Type")]
        public string CaseType { get; set; }
        [Required]
        [Display(Name = "Form Factor")]
        public string FormFactor { get; set; }
        [Required]
        [Display(Name = "Case Size")]
        public string CaseSize { get; set; }
        [Required]
        public string Color { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be a positive number.")]
        public int Quantity { get; set; }
        public string Image { get; set; }
    }
}
