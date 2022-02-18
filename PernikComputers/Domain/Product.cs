using PernikComputers.Domain.Enum;

namespace PernikComputers.Domain
{
    public class Product : CommonProperties
    {
        public string Description { get; set; }
        public Category Category { get; set; }
    }
}
