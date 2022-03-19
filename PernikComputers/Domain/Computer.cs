namespace PernikComputers.Domain
{
    public class Computer : CommonProperties
    {
        public string ProcessorId { get; set; }
        public Processor Processor { get; set; }
        public string MotherboardId { get; set; }
        public Motherboard Motherboard { get; set; }
        public string RamId { get; set; }
        public Ram Ram { get; set; }
        public string VideoCardId { get; set; }
        public VideoCard VideoCard { get; set; }
        public string PowerSupplyId { get; set; }
        public PowerSupply PowerSupply { get; set; }
        public string MemoryId { get; set; }
        public Memory Memory { get; set; }
        public string ComputerCaseId { get; set; }
        public ComputerCase ComputerCase { get; set; }
    }
}
