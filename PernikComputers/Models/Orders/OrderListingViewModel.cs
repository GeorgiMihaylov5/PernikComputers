using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PernikComputers.Models
{
    public class OrderListingViewModel
    {
        public string Id { get; set; }
        public string OrderedOn { get; set; }
        public string ProductId { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string Category { get; set; }
        public string ProductPrice { get; set; }
        public string TotalPrice { get; set; }
        public string CustomerId { get; set; }
        public string CustomerUsername { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
    }
}
