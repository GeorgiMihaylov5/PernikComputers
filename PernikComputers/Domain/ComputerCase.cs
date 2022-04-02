using System.Collections.Generic;

namespace PernikComputers.Domain
{
    public class ComputerCase : Product
    {
        public string CaseType { get; set; }
        public string FormFactor { get; set; }
        public string CaseSize { get; set; }
        public string Color { get; set; }
        public ICollection<Computer> Computers { get; set; }
    }
}
