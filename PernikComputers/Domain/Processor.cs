using System.Collections.Generic;

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
    }
}
