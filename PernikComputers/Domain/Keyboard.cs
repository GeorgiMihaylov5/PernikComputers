using PernikComputers.Domain.Enum;
using System.Collections.Generic;

namespace PernikComputers.Domain
{
    public class Keyboard : Product
    {
        public ConnectivityTechnology ConnectivityTechnology { get; set; }
        public int KeysCount { get; set; }
        public bool Backlight { get; set; }
        public double CableLength { get; set; }
        public string Size { get; set; }
        public override IEnumerable<string> FullDescription
          => new List<string>()
          {
                $"Keys Count: {this.KeysCount}",
                $"Backlight: {this.Backlight}",
                $"Cable Length: {this.CableLength} m",
                $"Size: {this.Size} mm",
                $"Warranty: {this.Warranty} months"
          };
        public override IEnumerable<string> PartialDescription
           => new List<string>()
            {
                $"Keys Count: {this.KeysCount}",
                $"Backlight: {this.Backlight}",
                $"Cable Length: {this.CableLength} m",
            };
    }
}
