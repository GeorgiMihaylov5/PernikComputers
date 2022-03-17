using PernikComputers.Domain.Enum;
using System.Collections.Generic;

namespace PernikComputers.Models
{
    public class ProductAllViewModel
    {
        public string Id { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public List<string> Description { get; set; }
        public Category Category { get; set; }
        public decimal Price { get; set; }
        public bool IsPromotion { get; set; }
        public string Image { get; set; }

    }
}
