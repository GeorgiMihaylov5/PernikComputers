using PernikComputers.Domain.Enum;

namespace PernikComputers.Domain
{
    public class Memory : CommonProperties
    {
        public MemoryType MemoryType { get; set; }
        public string FormFactor { get; set; }
        public int Capacity { get; set; }
        public int ReadSpeed { get; set; }
        public int WriteSpeed { get; set; }
    }
}
