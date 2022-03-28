using PernikComputers.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PernikComputers.Models
{
    public class ProductAllTableViewModel
    {
        public string Id { get; set; }
        public string Barcode { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public Category Category { get; set; }
        public decimal Price { get; set; }
        public int Promotion { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
        public int Warranty { get; set; }
        public string DetailsAction { get; set; }
    }
}
