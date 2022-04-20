using PernikComputers.Abstraction;
using PernikComputers.Data;
using PernikComputers.Domain;
using PernikComputers.Domain.Enum;
using System.Collections.Generic;
using System.Linq;

namespace PernikComputers.Service
{
    public class ComponentsService : IComponentService
    {
        private readonly ApplicationDbContext context;

        public ComponentsService(ApplicationDbContext _context)
        {
            this.context = _context;
        }

        //-------------Create------------
        public bool CreateProcessor(string socket, double cpuSpeed, double cpuBoostSpeed, int cores, int threads, int cache, string barcode, string manufacturer, string model, int warranty, decimal price, int quantity, string image)
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
                Manufacturer = manufacturer,
                Model = model,
                Category = Category.Processor,
                Price = price,
                Warranty = warranty,
                Quantity = quantity,
                Image = image
            };

            context.Processors.Add(processor);

            return context.SaveChanges() != 0;
        }

        public bool CreateMotherboard(string socket, string chipset, TypeRam typeRam, int ramSlotsCount, string formFactor, string barcode, string manufacturer, string model, int warranty, decimal price, int quantity, string image)
        {
            var motherboard = new Motherboard
            {
                Socket=socket,
                Chipset=chipset,
                TypeRam=typeRam,
                RamSlotsCount=ramSlotsCount,
                FormFactor=formFactor,
                Barcode = barcode,
                Manufacturer = manufacturer,
                Model = model,
                Category = Category.Motherboard,
                Price = price,
                Warranty = warranty,
                Quantity = quantity,
                Image = image
            };

            context.Motherboards.Add(motherboard);
            return context.SaveChanges() != 0;
        }

        public bool CreateRam(int size, TypeRam typeRam, int frequency, string timing, string barcode, string manufacturer, string model, int warranty, decimal price, int quantity, string image)
        {
            var ram = new Ram
            {
                Size=size,
                TypeRam = typeRam,
                Frequency=frequency,
                Timing=timing,
                Barcode = barcode,
                Manufacturer = manufacturer,
                Model = model,
                Category = Category.Ram,
                Price = price,
                Warranty = warranty,
                Quantity = quantity,
                Image = image
            };

            context.Rams.Add(ram);
            return context.SaveChanges() != 0;
        }

        public bool CreateVideoCard(ChipManufacturer chipManufacturer, string graphicProcessor, int sizeMemory, string typeMemory, int memoryFrequency, int coreFrequency, int currentProcesses, int railWidth, string slotType, string barcode, string manufacturer, string model, int warranty, decimal price, int quantity, string image)
        {
            var videoCard = new VideoCard 
            { 
                ChipManufacturer = chipManufacturer,
                GraphicProcessor = graphicProcessor,    
                SizeMemory = sizeMemory,
                TypeMemory = typeMemory,
                MemoryFrequency = memoryFrequency,
                CoreFrequency = coreFrequency,  
                CurrentProcesses = currentProcesses,
                RailWidth = railWidth,
                SlotType = slotType,
                Barcode = barcode,
                Manufacturer = manufacturer,
                Model = model,
                Category = Category.VideoCard,
                Price = price,
                Warranty = warranty,
                Quantity = quantity,
                Image = image
            };

            context.VideoCards.Add(videoCard);
            return context.SaveChanges() != 0;
        }

        public bool CreatePowerSupply(int power, string formFactor, int efficiency, string barcode, string manufacturer, string model, int warranty, decimal price, int quantity, string image)
        {
            var powerSupply = new PowerSupply
            {
                Power = power,
                FormFactor = formFactor,
                Efficiency = efficiency,
                Barcode = barcode,
                Manufacturer = manufacturer,
                Model = model,
                Category = Category.PowerSupply,
                Price = price,
                Warranty = warranty,
                Quantity = quantity,
                Image = image
            };

            context.PowerSupplies.Add(powerSupply);
            return context.SaveChanges() != 0;
        }

        public bool CreateMemory(MemoryType memoryType, string formFactor, int capacity, int readSpeed, int writeSpeed, string barcode, string manufacturer, string model, int warranty, decimal price, int quantity, string image)
        {
            var memory = new Memory
            {
                MemoryType = memoryType,
                FormFactor = formFactor,
                Capacity = capacity,
                ReadSpeed = readSpeed,
                WriteSpeed = writeSpeed,
                Barcode = barcode,
                Manufacturer = manufacturer,
                Model = model,
                Category = Category.Memory,
                Price = price,
                Warranty = warranty,
                Quantity = quantity,
                Image = image
            };

            context.Memories.Add(memory);
            return context.SaveChanges() != 0;
        }

        public bool CreateComputerCase(string caseType, string formFactor, string caseSize, string color, string barcode, string manufacturer, string model, int warranty, decimal price, int quantity, string image)
        {
            var computerCase = new ComputerCase
            {
                CaseType = caseType,
                FormFactor = formFactor,
                CaseSize = caseSize,
                Color = color,
                Barcode = barcode,
                Manufacturer = manufacturer,
                Model = model,
                Category = Category.ComputerCase,
                Price = price,
                Warranty = warranty,
                Quantity = quantity,
                Image = image
            };

            context.ComputerCases.Add(computerCase);
            return context.SaveChanges() != 0;
        }

        //------------List------------
        public List<Product> GetComponents()
        {
            return context.Products.Where(x => x.Category != Category.Computer &&
            x.Category != Category.Laptop && x.Category != Category.Periphery &&
            x.Category != Category.Accessories && x.IsRemoved != true).ToList();
        }
        
        //------------------Update---------------

        public bool UpdateProcessor(string id, string socket, double cpuSpeed, double cpuBoostSpeed, int cores, int threads, int cache, string barcode, string manufacturer, string model, int warranty, decimal price, int quantity, string image)
        {
            var item = context.Processors.Find(id);

            if (item == null)
            {
                return false;
            }
            item.Socket = socket;
            item.CPUSpeed = cpuSpeed;
            item.CPUBoostSpeed = cpuBoostSpeed;
            item.Cores = cores;
            item.Threads = threads;
            item.Cache = cache;
            item.Barcode = barcode;
            item.Manufacturer = manufacturer;
            item.Model = model;
            item.Price = price;
            item.Warranty = warranty;
            item.Quantity = quantity;
            item.Image = image;

            context.Update(item);

            return context.SaveChanges() != 0;
        }

        public bool UpdateMotherboard(string id, string socket, string chipset, TypeRam typeRam, int ramSlotsCount, string formFactor, string barcode, string manufacturer, string model, int warranty, decimal price, int quantity, string image)
        {
            var item = context.Motherboards.Find(id);

            if (item == null)
            {
                return false;
            }

            item.Socket = socket;
            item.Chipset = chipset;
            item.TypeRam = typeRam;
            item.RamSlotsCount = ramSlotsCount;
            item.FormFactor = formFactor;
            item.Barcode = barcode;
            item.Manufacturer = manufacturer;
            item.Model = model;
            item.Price = price;
            item.Warranty = warranty;
            item.Quantity = quantity;
            item.Image = image;

            context.Update(item);

            return context.SaveChanges() != 0;
        }

        public bool UpdateRam(string id, int size, TypeRam typeRam, int frequency, string timing, string barcode, string manufacturer, string model, int warranty, decimal price, int quantity, string image)
        {
            var item = context.Rams.Find(id);

            if (item == null)
            {
                return false;
            }

            item.Size = size;
            item.TypeRam = typeRam;
            item.Frequency = frequency;
            item.Timing = timing;
            item.Barcode = barcode;
            item.Manufacturer = manufacturer;
            item.Model = model;
            item.Price = price;
            item.Warranty = warranty;
            item.Quantity = quantity;
            item.Image = image;

            context.Update(item);

            return context.SaveChanges() != 0;
        }

        public bool UpdateVideoCard(string id, ChipManufacturer chipManufacturer, string graphicProcessor, int sizeMemory, string typeMemory, int memoryFrequency, int coreFrequency, int currentProcesses, int railWidth, string slotType, string barcode, string manufacturer, string model, int warranty, decimal price, int quantity, string image)
        {
            var item = context.VideoCards.Find(id);

            if (item == null)
            {
                return false;
            }

            item.ChipManufacturer = chipManufacturer;
            item.GraphicProcessor = graphicProcessor;
            item.SizeMemory = sizeMemory;
            item.TypeMemory = typeMemory;
            item.MemoryFrequency = memoryFrequency;
            item.CoreFrequency = coreFrequency;
            item.CurrentProcesses = currentProcesses;
            item.RailWidth = railWidth;
            item.SlotType = slotType;
            item.Barcode = barcode;
            item.Manufacturer = manufacturer;
            item.Model = model;
            item.Price = price;
            item.Warranty = warranty;
            item.Quantity = quantity;
            item.Image = image;

            context.Update(item);

            return context.SaveChanges() != 0;
        }

        public bool UpdatePowerSupply(string id, int power, string formFactor, int efficiency, string barcode, string manufacturer, string model, int warranty, decimal price, int quantity, string image)
        {
            var item = context.PowerSupplies.Find(id);

            if (item == null)
            {
                return false;
            }

            item.Power = power;
            item.FormFactor = formFactor;
            item.Efficiency = efficiency;
            item.Barcode = barcode;
            item.Manufacturer = manufacturer;
            item.Model = model;
            item.Price = price;
            item.Warranty = warranty;
            item.Quantity = quantity;
            item.Image = image;

            context.Update(item);

            return context.SaveChanges() != 0;
        }

        public bool UpdateMemory(string id, MemoryType memoryType, string formFactor, int capacity, int readSpeed, int writeSpeed, string barcode, string manufacturer, string model, int warranty, decimal price, int quantity, string image)
        {
            var item = context.Memories.Find(id);

            if (item == null)
            {
                return false;
            }

            item.MemoryType = memoryType;
            item.FormFactor = formFactor;
            item.Capacity = capacity;
            item.ReadSpeed = readSpeed;
            item.WriteSpeed = writeSpeed;
            item.Barcode = barcode;
            item.Manufacturer = manufacturer;
            item.Model = model;
            item.Price = price;
            item.Warranty = warranty;
            item.Quantity = quantity;
            item.Image = image;

            context.Update(item);

            return context.SaveChanges() != 0;
        }

        public bool UpdateComputerCase(string id, string caseType, string formFactor, string caseSize, string color, string barcode, string manufacturer, string model, int warranty, decimal price, int quantity, string image)
        {
            var item = context.ComputerCases.Find(id);

            if (item == null)
            {
                return false;
            }

            item.CaseType = caseType;
            item.FormFactor = formFactor;
            item.CaseSize = caseSize;
            item.Color = color;
            item.Barcode = barcode;
            item.Manufacturer = manufacturer;
            item.Model = model;
            item.Price = price;
            item.Warranty = warranty;
            item.Quantity = quantity;
            item.Image = image;

            context.Update(item);

            return context.SaveChanges() != 0;
        }
    }
}
