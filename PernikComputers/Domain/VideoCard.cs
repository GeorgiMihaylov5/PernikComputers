using PernikComputers.Domain.Enum;

namespace PernikComputers.Domain
{
    public class VideoCard : CommonProperties
    {
        public ChipManufacturer ChipManufacturer { get; set; }
        public string GraphicProcessor{ get; set; }
        public int SizeMemory { get; set; }
        public string TypeMemory { get; set; }
        public int MemoryFrequency { get; set; }
        public int CoreFrequency { get; set; }
        public int CurrentProcessors { get; set; }
        public int RailWidth { get; set; }
        public string Slot { get; set; }
    }
}
