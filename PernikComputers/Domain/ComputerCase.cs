using System.Collections.Generic;

namespace PernikComputers.Domain
{
    public class ComputerCase : Product
    {
        public string CaseType { get; set; }
        public string FormFactor { get; set; }
        public string CaseSize { get; set; }
        public string Color { get; set; }
        public virtual ICollection<Computer> Computers { get; set; }
        public override IEnumerable<string> FullDescription
            => new List<string>()
            {
                $"Type: {this.CaseType} W",
                $"Form factor: {this.FormFactor}",
                $"Size: {this.CaseSize} mm",
                $"Color: {this.Color }",
                $"Warranty: {this.Warranty} months"
            };
        public override IEnumerable<string> PartialDescription
            => new List<string>()
            {
                 $"Type: {this.CaseType} W",
                 $"Form factor: {this.FormFactor}",
                 $"Size: {this.CaseSize} mm",
            };
    }
}
