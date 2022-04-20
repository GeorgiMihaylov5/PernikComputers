using PernikComputers.Domain.Enum;
using System.Collections.Generic;

namespace PernikComputers.Domain
{
    public class Monitor : Product
    {
        public int Size { get; set; }
        public string Resolution { get; set; }
        public TypeDisplay TypeDisplay { get; set; }
        public int ReactionTime { get; set; }
        public int RefreshRate { get; set; }
        public override IEnumerable<string> FullDescription 
            => new List<string>()
            {
                $"Size: {this.Size}\"",
                $"Resolution: {this.Resolution}",
                $"Type Display: {this.TypeDisplay}",
                $"Reaction Time: {this.ReactionTime} ms",
                $"Refresh Rate: {this.RefreshRate} Hz",
                $"Warranty: {this.Warranty} months"
            };
        public override IEnumerable<string> PartialDescription
            => new List<string>()
            {
                $"Size: {this.Size}\"",
                $"Resolution: {this.Resolution}",
                $"Type Display: {this.TypeDisplay}",
                $"Refresh Rate: {this.RefreshRate} Hz",
            };
    }
}
