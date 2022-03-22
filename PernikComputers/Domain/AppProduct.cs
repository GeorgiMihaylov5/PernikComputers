using PernikComputers.Domain.Enum;

namespace PernikComputers.Domain
{
    public class AppProduct : Product
    {
        public string Description { get; set; }
        public Category Category { get; set; }
    }
}
