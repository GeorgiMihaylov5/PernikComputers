using System.Collections.Generic;

namespace PernikComputers.Domain
{
    public class PowerSupply : Product
    {
        public int Power { get; set; }
        public string FormFactor { get; set; }
        public int Efficiency { get; set; }
        public ICollection<Computer> Computers { get; set; }
    }
}
