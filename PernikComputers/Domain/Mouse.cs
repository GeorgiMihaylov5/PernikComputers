using PernikComputers.Domain.Enum;
using System.Collections.Generic;

namespace PernikComputers.Domain
{
    public class Mouse : Product
    {
        public ConnectivityTechnology ConnectivityTechnology { get; set; }
        public int KeysCount { get; set; }
        public bool Backlight { get; set; }
        public double CableLength { get; set; }
        public string Size { get; set; }
        public int Sensitivity { get; set; }
        public double Weight { get; set; }
        public override IEnumerable<string> FullDescription
            => new List<string>()
            {
                $"Keys Count: {this.KeysCount}",
                $"Backlight: {this.Backlight}",
                $"Cable Length: {this.CableLength} m",
                $"Sensitivity: {this.Sensitivity} dpi",
                $"Weight: {this.Weight} grams",
                $"Size: {this.Size} mm",
                $"Warranty: {this.Warranty} months"
            };
        public override IEnumerable<string> PartialDescription
           => new List<string>()
            {
                $"Keys Count: {this.KeysCount}",
                $"Backlight: {this.Backlight}",
                $"Sensitivity: {this.Sensitivity} dpi",
            };
    }
}
