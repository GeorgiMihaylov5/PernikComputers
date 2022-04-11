using System.ComponentModel.DataAnnotations.Schema;

namespace PernikComputers.Domain
{
    public class Computer : Product
    {
        [ForeignKey("Products")]
        public string ProcessorId { get; set; }
        public virtual Processor Processor { get; set; }
        [ForeignKey("Motherboards")]
        public string MotherboardId { get; set; }
        public virtual Motherboard Motherboard { get; set; }
        [ForeignKey("Rams")]
        public string RamId { get; set; }
        public virtual Ram Ram { get; set; }
        [ForeignKey("VideoCards")]
        public string VideoCardId { get; set; }
        public virtual VideoCard VideoCard { get; set; }
        [ForeignKey("PowerSupplies")]
        public string PowerSupplyId { get; set; }
        public virtual PowerSupply PowerSupply { get; set; }
        [ForeignKey("Memories")]
        public string MemoryId { get; set; }
        public virtual Memory Memory { get; set; }
        [ForeignKey("ComputerCases")]
        public string ComputerCaseId { get; set; }
        public virtual ComputerCase ComputerCase { get; set; }
    }
}
