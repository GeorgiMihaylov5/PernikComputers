using PernikComputers.Domain.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PernikComputers.Domain
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Barcode { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public int Warranty { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public Category Category { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; }
        [NotMapped()]
        public virtual IEnumerable<string> FullDescription { get; }
        [NotMapped()]
        public virtual IEnumerable<string> PartialDescription { get; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
