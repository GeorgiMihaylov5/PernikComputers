﻿using PernikComputers.Domain.Enum;
using System.Collections.Generic;

namespace PernikComputers.Domain
{
    public class Motherboard : Product
    {
        public string Socket { get; set; }
        public string Chipset { get; set; }
        public TypeRam TypeRam { get; set; }
        public int RamSlotsCount { get; set; }
        public string FormFactor { get; set; }
        public ICollection<Computer> Computers { get; set; }
    }
}
