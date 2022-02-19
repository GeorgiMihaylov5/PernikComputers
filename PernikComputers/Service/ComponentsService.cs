using PernikComputers.Abstraction;
using PernikComputers.Data;
using PernikComputers.Domain;
using PernikComputers.Domain.Enum;
using System.Collections.Generic;
using System.Linq;

namespace PernikComputers.Service
{
    public class ComponentsService : IComponentsService
    {
        private readonly ApplicationDbContext context;

        public ComponentsService(ApplicationDbContext _context)
        {
            this.context = _context;
        }

        public bool CreateProcessor(string socket, double cpuSpeed, double cpuBoostSpeed, int cores, int threads, int cache, string barcode, string name, string manufacturer, string model, int warranty, decimal price, int quantity, string image)
        {
            var processor = new Processor
            {
                Socket = socket,
                CPUSpeed = cpuSpeed,
                CPUBoostSpeed = cpuBoostSpeed,
                Cores = cores,
                Threads = threads,
                Cache = cache,
                Barcode = barcode,
                Name = name,
                Manufacturer = manufacturer,
                Model = model,
                Warranty = warranty,
                Quantity = quantity,
                Image = image
            };

            context.Processors.Add(processor);

            return context.SaveChanges() != 0;
        }

        bool IComponentsService.CreateMotherboard(string socket, string chipset, TypeRam typeRam, int ramSlotsCount, string formFactor, string barcode, string name, string manufacturer, string model, int warranty, decimal price, int quantity, string image)
        {
            var motherboard = new Motherboard
            {
                Socket=socket,
                Chipset=chipset,
                TypeRam=typeRam,
                RamSlotsCount=ramSlotsCount,
                FormFactor=formFactor,
                Barcode = barcode,
                Name = name,
                Manufacturer = manufacturer,
                Model = model,
                Warranty = warranty,
                Quantity = quantity,
                Image = image
            };

            context.Motherboards.Add(motherboard);
            return context.SaveChanges() != 0;
        }

        bool IComponentsService.CreateRam(int size, TypeRam typeRam, int frequency, string timing, string barcode, string name, string manufacturer, string model, int warranty, decimal price, int quantity, string image)
        {
            var ram = new Ram
            {
                Size=size,
                TypeRam = typeRam,
                Frequency=frequency,
                Timing=timing,
                Barcode = barcode,
                Name = name,
                Manufacturer = manufacturer,
                Model = model,
                Warranty = warranty,
                Quantity = quantity,
                Image = image
            };

            context.Rams.Add(ram);
            return context.SaveChanges() != 0;
        }

        public List<Processor> GetProcessors()
        {
            return context.Processors.ToList();
        }

        public List<Motherboard> GetMotherboards()
        {
            return context.Motherboards.ToList();
        }

        public List<Ram> GetRams()
        {
            return context.Rams.ToList();
        }
    }
}
