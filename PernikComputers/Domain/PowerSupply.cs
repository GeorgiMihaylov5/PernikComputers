using System.Collections.Generic;

namespace PernikComputers.Domain
{
    public class PowerSupply : Product
    {
        public int Power { get; set; }
        public string FormFactor { get; set; }
        public int Efficiency { get; set; }
        public virtual ICollection<Computer> Computers { get; set; }
        public override IEnumerable<string> FullDescription
            => new List<string>()
            {
                $"Power: {this.Power} W",
                $"Form factor: {this.FormFactor}",
                $"Efficiency: {this.Efficiency}%",
                $"Warranty: {this.Warranty} months"
            };
        public override IEnumerable<string> PartialDescription
            => new List<string>()
            {
                 $"Power: {this.Power} W",
                 $"Efficiency: {this.Efficiency}%"
            };
    }
}
