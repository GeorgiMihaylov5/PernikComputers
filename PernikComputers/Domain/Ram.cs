using PernikComputers.Domain.Enum;
using System.Collections.Generic;

namespace PernikComputers.Domain
{
    public class Ram: Product
    {
        public int Size { get; set; }
        public TypeRam TypeRam { get; set; }
        public int Frequency { get; set; }
        public string Timing { get; set; }
        public virtual ICollection<Computer> Computers { get; set; }
        public override IEnumerable<string> FullDescription
            => new List<string>()
            {
                $"Capacity: {this.Size} GB",
                $"Type: {this.TypeRam}",
                $"Frequency: {this.Frequency} MHz",
                $"Timing: {this.Timing}",
                $"Warranty: {this.Warranty} months"
            };
        public override IEnumerable<string> PartialDescription
            => new List<string>()
            {
                $"Capacity: {this.Size} GB",
                $"Type: {this.TypeRam}",
                $"Frequency: {this.Frequency} MHz"
            };
    }
}
