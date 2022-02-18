using PernikComputers.Domain.Enum;

namespace PernikComputers.Domain
{
    public class Ram: CommonProperties
    {
        public int Size { get; set; }
        public TypeRam TypeRam { get; set; }
        public int Frequency { get; set; }
        public string Timing { get; set; }
    }
}
