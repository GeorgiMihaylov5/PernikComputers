using PernikComputers.Domain.Enum;
using System.Collections.Generic;

namespace PernikComputers.Domain
{
    public class Memory : Product
    {
        public MemoryType MemoryType { get; set; }
        public string FormFactor { get; set; }
        public int Capacity { get; set; }
        public int ReadSpeed { get; set; }
        public int WriteSpeed { get; set; }
        public virtual ICollection<Computer> Computers { get; set; }
    }
}
