using PernikComputers.Domain.Enum;
using System.Collections.Generic;

namespace PernikComputers.Domain
{
    public class Motherboard : Product
    {
        public string Socket { get; set; }
        public string Chipset { get; set; }
        public TypeRam TypeRam { get; set; }
        public int RamSlotsCount { get; set; }
        public string FormFactor { get; set; }
        public virtual ICollection<Computer> Computers { get; set; }
        public override IEnumerable<string> FullDescription
            => new List<string>()
            {
                $"Socket: {this.Socket}",
                $"Chipset: {this.Chipset}",
                $"Supported memory: {this.TypeRam}",
                $"Number of memory slots: {this.RamSlotsCount}",
                $"Form factor: {this.FormFactor}",
                $"Warranty: {this.Warranty} months"
            };
        public override IEnumerable<string> PartialDescription
            => new List<string>()
            {
                $"Socket: {this.Socket}",
                $"Chipset: {this.Chipset}",
                $"Supported memory: {this.TypeRam}",
            };
    }
}
