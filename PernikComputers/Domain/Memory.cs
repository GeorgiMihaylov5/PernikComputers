using PernikComputers.Domain.Enum;
using System.Collections.Generic;

namespace PernikComputers.Domain
{
    public class Memory : Product
    {
        public MemoryType MemoryType { get; set; }
        public string FormFactor { get; set; }
        public int Capacity { get; set; }
        public int ReadSpeed { get; set; }
        public int WriteSpeed { get; set; }
        public virtual ICollection<Computer> Computers { get; set; }
        public override IEnumerable<string> FullDescription
            => new List<string>()
            {
                $"Type: {this.MemoryType} W",
                $"Form factor: {this.FormFactor}",
                $"Capacity: {this.Capacity} GB",
                $"Reading speed: {this.ReadSpeed} MB/s",
                $"Recording speed: {this.WriteSpeed} MB/s",
                $"Warranty: {this.Warranty} months"
            };
        public override IEnumerable<string> PartialDescription
            => new List<string>()
            {
                $"Type: {this.MemoryType} W",
                $"Capacity: {this.Capacity} GB",
            };
    }
}
