using System.ComponentModel.DataAnnotations.Schema;

namespace PernikComputers.Domain
{
    public class CommonProperties
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Barcode { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public int Warranty { get; set; }
        public decimal Price { get; set; }
        public bool IsPromotion { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; }
    }
}
