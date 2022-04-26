using System.Collections.Generic;

namespace PernikComputers.Domain
{
    public class Laptop : Product
    {
        public string Processor { get; set; }
        public string Motherboard { get; set; }
        public string Ram { get; set; }
        public string VideoCard { get; set; }
        public string PowerSupply { get; set; }
        public string Memory { get; set; }
        public string Resolution { get; set; }
        public int RefreshRate { get; set; }
        public double DisplaySize { get; set; }
        public string Color { get; set; }
        public override IEnumerable<string> FullDescription
            => new List<string>()
            {
                 $"Processor: {this.Processor}",
                 $"Video Card: {this.VideoCard}",
                 $"Ram: {this.Ram}",
                 $"Connection and Porst: {this.Motherboard}",
                 $"Memory: {this.Memory}",
                 $"Power Supply: {this.PowerSupply}",
                 $"Resolution: {this.Resolution}",
                 $"Refresh Rate: {this.RefreshRate} Hz",
                 $"Display Size: {this.DisplaySize}\"",
                 $"Color: {this.Color}",
                 $"Warranty: {this.Warranty} months"
            };
        public override IEnumerable<string> PartialDescription
            => new List<string>()
            {
                 $"{this.Processor}",
                 $"{this.VideoCard}",
                 $"{this.Ram}",
                 $"{this.Memory}",
                 $"{this.Resolution}",
                 $"{this.DisplaySize}\"",
            };
    }
}
