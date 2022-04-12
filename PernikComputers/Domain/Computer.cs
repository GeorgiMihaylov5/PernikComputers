using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PernikComputers.Domain
{
    public class Computer : Product
    {
        public string ProcessorId { get; set; }
        public virtual Processor Processor { get; set; }
        public string MotherboardId { get; set; }
        public virtual Motherboard Motherboard { get; set; }
        public string RamId { get; set; }
        public virtual Ram Ram { get; set; }
        public string VideoCardId { get; set; }
        public virtual VideoCard VideoCard { get; set; }
        public string PowerSupplyId { get; set; }
        public virtual PowerSupply PowerSupply { get; set; }
        public string MemoryId { get; set; }
        public virtual Memory Memory { get; set; }
        public string ComputerCaseId { get; set; }
        public virtual ComputerCase ComputerCase { get; set; }

        public override IEnumerable<string> FullDescription
            => new List<string>()
            {
                 $"Processor: {this.Processor.Manufacturer} {this.Processor.Model} ({this.Processor.CPUSpeed}/{this.Processor.CPUBoostSpeed} GHz, {this.Processor.Cache} M, {this.Processor.Cores} cores, {this.Processor.Threads} threads)",
                 $"Video Card: {this.VideoCard.Manufacturer} {this.VideoCard.Model} {this.VideoCard.SizeMemory} GB",
                 $"Ram: {this.Ram.Size} GB {this.Ram.TypeRam} {this.Ram.Frequency} MHz",
                 $"Chipset: {this.Motherboard.Chipset}",
                 $"Motherboard: {this.Motherboard.Manufacturer} {this.Motherboard.Model}",
                 $"Memory: {this.Memory.Capacity} GB {this.Memory.MemoryType}",
                 $"Computer case: {this.ComputerCase.Manufacturer} {this.ComputerCase.Model}",
                 $"Size: {this.ComputerCase.CaseSize} mm",
                 $"Power Supply: {this.PowerSupply.Power} W {this.PowerSupply.Manufacturer} {this.PowerSupply.Model}",
                 $"Warranty: {this.Warranty} months"
            };
        public override IEnumerable<string> PartialDescription
            => new List<string>()
            {
                $"{this.Processor.Manufacturer} {this.Processor.Model} ({this.Processor.CPUSpeed}/{this.Processor.CPUBoostSpeed} GHz, {this.Processor.Cache} M)",
                $"{this.VideoCard.Manufacturer} {this.VideoCard.Model} {this.VideoCard.SizeMemory} GB",
                $"{this.Ram.Size} GB {this.Ram.TypeRam} {this.Ram.Frequency} MHz"
            };
    }
}
