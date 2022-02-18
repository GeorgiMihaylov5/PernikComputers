using PernikComputers.Domain.Enum;

namespace PernikComputers.Domain
{
    public class Motherboard : CommonProperties
    {
        public string Socket { get; set; }
        public string Chipset { get; set; }
        public TypeRam TypeRam { get; set; }
        public int RamSlotsCount { get; set; }
        public string FormFactor { get; set; }
    }
}
