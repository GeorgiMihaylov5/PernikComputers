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
    }
}
