using PernikComputers.Domain.Enum;
using System.Collections.Generic;

namespace PernikComputers.Domain
{
    public class Ram: Product
    {
        public int Size { get; set; }
        public TypeRam TypeRam { get; set; }
        public int Frequency { get; set; }
        public string Timing { get; set; }
        public virtual ICollection<Computer> Computers { get; set; }
    }
}
