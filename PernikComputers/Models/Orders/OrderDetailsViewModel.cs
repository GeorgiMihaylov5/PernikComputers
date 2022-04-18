using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PernikComputers.Models
{
    public class OrderDetailsViewModel
    {
        public string Id { get; set; }
        public string OrderedOn { get; set; }
        public string ProductId { get; set; }
        public string Barcode { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }
        public string Warranty { get; set; }
        public string SinglePrice { get; set; }
        public string OrderedPrice { get; set; }
        public string CustomerId { get; set; }
        public string CustomerUsername { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
    }
}