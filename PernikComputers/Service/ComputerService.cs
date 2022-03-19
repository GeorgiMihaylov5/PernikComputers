using PernikComputers.Abstraction;
using PernikComputers.Data;
using PernikComputers.Domain;
using System.Collections.Generic;
using System.Linq;

namespace PernikComputers.Service
{
    public class ComputerService : IComputerService
    {
        private readonly ApplicationDbContext context;

        public ComputerService(ApplicationDbContext _context)
        {
            this.context = _context;
        }

        public bool Create(string processorId, string motherboardId, string ramId, string videoCardId, 
            string powerSupplyId, string memoryId, string computerCaseId, 
            string barcode, string manufacturer, string model, int warranty, decimal price, int quantity, string image)
        {
            var computer = new Computer
            {
                ProcessorId = processorId,
                MotherboardId = motherboardId,
                RamId = ramId,
                VideoCardId = videoCardId,
                PowerSupplyId = powerSupplyId,
                MemoryId = memoryId,
                ComputerCaseId = computerCaseId,
                Barcode = barcode,
                Manufacturer = manufacturer,
                Model = model,
                Price = price,
                Warranty = warranty,
                Quantity = quantity,
                Image = image
            };
            context.Computers.Add(computer);
            return context.SaveChanges() != 0;
        }

        public Computer GetComputer(string id)
        {
            var computer = context.Computers.Find(id);

            computer.Processor = context.Processors.Find(computer.ProcessorId);
            computer.Motherboard = context.Motherboards.Find(computer.MotherboardId);
            computer.Ram = context.Rams.Find(computer.RamId);
            computer.VideoCard = context.VideoCards.Find(computer.VideoCardId);
            computer.PowerSupply = context.PowerSupplies.Find(computer.PowerSupplyId);
            computer.Memory = context.Memories.Find(computer.MemoryId);
            computer.ComputerCase = context.ComputerCases.Find(computer.ComputerCaseId);

            return computer;
        }

        public List<Computer> GetComputers()
        {
            var computers = context.Computers.ToList();

            foreach (var computer in computers)
            {
                computer.Processor = context.Processors.Find(computer.ProcessorId);
                computer.Motherboard = context.Motherboards.Find(computer.MotherboardId);
                computer.Ram = context.Rams.Find(computer.RamId);
                computer.VideoCard = context.VideoCards.Find(computer.VideoCardId);
                computer.PowerSupply = context.PowerSupplies.Find(computer.PowerSupplyId);
                computer.Memory = context.Memories.Find(computer.MemoryId);
                computer.ComputerCase = context.ComputerCases.Find(computer.ComputerCaseId);
            }
            return computers;
        }

        public bool RemoveComputer(string id)
        {
            var computer = context.Computers.Find(id);

            if (computer == null)
            {
                return false;
            }

            context.Computers.Remove(computer);

            return context.SaveChanges() != 0;
        }

        public bool UpdateComputer(string id, string processorId, string motherboardId, string ramId, string videoCardId, string powerSupplyId, string memoryId, string computerCaseId, string barcode, string manufacturer, string model, int warranty, decimal price, int quantity, string image)
        {
            var computer = context.Computers.Find(id);

            if (computer == null)
            {
                return false;
            }
            computer.ProcessorId = processorId;
            computer.MotherboardId = motherboardId;
            computer.RamId = ramId;
            computer.VideoCardId = videoCardId;
            computer.PowerSupplyId = powerSupplyId;
            computer.MemoryId = memoryId;
            computer.ComputerCaseId = computerCaseId;
            computer.Barcode = barcode;
            computer.Manufacturer = manufacturer;
            computer.Model = model;
            computer.Price = price;
            computer.Warranty = warranty;
            computer.Quantity = quantity;
            computer.Image = image;

            context.Computers.Update(computer);
            return context.SaveChanges() != 0;
        }
    }
}
