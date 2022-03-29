using PernikComputers.Domain.Enum;
using System.Collections.Generic;

namespace PernikComputers.Models
{
    public class ProductDetailsViewModel
    {
        public string Id { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string Barcode { get; set; }
        public List<string> Description { get; set; }
        public Category Category { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int Promotion { get; set; }
        public string Image { get; set; }
    }
}
