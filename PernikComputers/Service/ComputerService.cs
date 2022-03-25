using Microsoft.EntityFrameworkCore;
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
                Image = image,
                Processor = context.Processors.Find(processorId),
                Motherboard = context.Motherboards.Find(motherboardId),
                Ram = context.Rams.Find(ramId),
                VideoCard = context.VideoCards.Find(videoCardId),
                PowerSupply = context.PowerSupplies.Find(powerSupplyId),
                Memory = context.Memories.Find(memoryId),
                ComputerCase = context.ComputerCases.Find(computerCaseId)
            };
            context.Computers.Add(computer);
            return context.SaveChanges() != 0;
        }

        public Computer GetComputer(string id)
        {
            return context.Computers.Include(x => x.Processor)
                .Include(x => x.Motherboard)
                .Include(x => x.Ram)
                .Include(x => x.VideoCard)
                .Include(x => x.PowerSupply)
                .Include(x => x.Memory)
                .Include(x => x.ComputerCase).FirstOrDefault(x => x.Id == id);
        }

        public List<Computer> GetComputers()
        {
            return context.Computers
                .Include(x => x.Processor)
                .Include(x => x.Motherboard)
                .Include(x => x.Ram)
                .Include(x => x.VideoCard)
                .Include(x => x.PowerSupply)
                .Include(x => x.Memory)
                .Include(x => x.ComputerCase).ToList();
        }

        //public bool RemoveComputer(string id)
        //{
        //    var computer = context.Computers.Find(id);

        //    if (computer == null)
        //    {
        //        return false;
        //    }

        //    context.Computers.Remove(computer);

        //    return context.SaveChanges() != 0;
        //}

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
