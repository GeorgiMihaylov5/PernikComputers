using PernikComputers.Domain.Enum;
using System.Collections.Generic;

namespace PernikComputers.Models
{
    public class ProductAllViewModel
    {
        public string Id { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public IEnumerable<string> Description { get; set; }
        public Category Category { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
    }
}
