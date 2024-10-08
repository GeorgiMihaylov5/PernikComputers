﻿using System.Collections.Generic;

namespace PernikComputers.Domain
{
    public class Processor: Product
    {
        public string Socket { get; set; }
        public double CPUSpeed { get; set; }
        public double CPUBoostSpeed { get; set; }
        public int Cores { get; set; }
        public int Threads { get; set; }
        public int Cache { get; set; }
        public virtual ICollection<Computer> Computers { get; set; }
        public override IEnumerable<string> FullDescription
            => new List<string>()
            {
                $"Socket: {this.Socket}",
                $"Operating frequency: {this.CPUSpeed} GHz",
                $"Turbo Boost: {this.CPUBoostSpeed} GHz",
                $"Cores: {this.Cores}",
                $"Threads: {this.Threads}",
                $"Cashe: {this.Cache} MB",
                $"Warranty: {this.Warranty} months"
            };
        public override IEnumerable<string> PartialDescription
            => new List<string>() {
                $"Socket: {this.Socket}",
                $"Operating frequency: {this.CPUSpeed} GHz",
                $"Turbo Boost: {this.CPUBoostSpeed} GHz",
                $"Cores: {this.Cores}",
                $"Threads: {this.Threads}",
            };

    }
}
