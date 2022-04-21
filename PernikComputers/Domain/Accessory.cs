using PernikComputers.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PernikComputers.Domain
{
    public class Accessory : Product
    {
        public string TypeAccessory { get; set; }
        public string Description { get; set; }
        public override IEnumerable<string> FullDescription
            => new List<string>()
            {
                $"Type: {this.TypeAccessory}",
                $"Warranty: {this.Warranty} months",
                $"Other: {Description}"
            };

        public override IEnumerable<string> PartialDescription
             => new List<string>()
             {
                $"Type: {this.TypeAccessory}",
                $"Warranty: {this.Warranty} months"
             };
    }
}
