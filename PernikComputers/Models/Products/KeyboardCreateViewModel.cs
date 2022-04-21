using PernikComputers.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PernikComputers.Models
{
    public class KeyboardCreateViewModel
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
        public ConnectivityTechnology ConnectivityTechnology { get; set; }
        public int KeysCount { get; set; }
        [Required]
        public bool Backlight { get; set; }
        [Required]
        public double CableLength { get; set; }
        [Required]
        public string Size { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be a positive number.")]
        public int Quantity { get; set; }
        public string Image { get; set; }
    }
}
