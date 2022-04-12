using PernikComputers.Domain.Enum;
using System.Collections.Generic;

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
        public virtual ICollection<Computer> Computers { get; set; }
        public override IEnumerable<string> FullDescription
            => new List<string>()
            {   
                $"Chip manufacturer: {this.ChipManufacturer }",
                $"Graphic Processor: {this.GraphicProcessor}",
                $"Memory capacity: {this.SizeMemory } GB",
                $"Memory type: {this.TypeMemory}",
                $"Memory Frequency: {this.MemoryFrequency} MHz",
                $"Core Frequency: {this.CoreFrequency} MHz",
                $"Current Processes: {this.CurrentProcesses}",
                $"Rail Width: {this.RailWidth} bit",
                $"Slot: {this.SlotType}",
                $"Warranty: {this.Warranty} months"
            };
        public override IEnumerable<string> PartialDescription
            => new List<string>()
            {
                $"Graphic Processor: {this.GraphicProcessor}",
                $"Memory capacity: {this.SizeMemory } GB",
                $"Memory type: {this.TypeMemory}",
            };
    }
}
