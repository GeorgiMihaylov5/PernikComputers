using PernikComputers.Domain.Enum;

namespace PernikComputers.Domain
{
    public class VideoCard : Product
    {
        public ChipManufacturer ChipManufacturer { get; set; }
        public string GraphicProcessor{ get; set; }
        public int SizeMemory { get; set; }
        public string TypeMemory { get; set; }
        public int MemoryFrequency { get; set; }
        public int CoreFrequency { get; set; }
        public int CurrentProcesses { get; set; }
        public int RailWidth { get; set; }
        public string SlotType { get; set; }
    }
}
