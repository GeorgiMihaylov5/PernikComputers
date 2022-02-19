namespace PernikComputers.Domain
{
    public class Computer
    {
        public string ProcessorId { get; set; }
        public Processor Processor { get; set; }
        public string MotherboardId { get; set; }
        public Motherboard Motherboard { get; set; }
        public string RamId { get; set; }
        public Ram Ram { get; set; }
    }
}
