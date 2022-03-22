using PernikComputers.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PernikComputers.Domain
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public DateTime OrderedOn { get; set; }
        public Category Category { get; set; }
        public string ProductId { get; set; }
        public Product Product { get; set; }
        public string CustomerId { get; set; }
        public ApplicationUser Customer { get; set; }
        //public string Manufacturer { get; set; }
        //public string Model { get; set; }
        //public decimal Price { get; set; }
        public int Count { get; set; }
        public OrderStatus Status { get; set; }
        public string Notes { get; set; }
    }
}
