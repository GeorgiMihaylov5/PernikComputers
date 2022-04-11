using Microsoft.EntityFrameworkCore;
using PernikComputers.Abstraction;
using PernikComputers.Data;
using PernikComputers.Domain;
using PernikComputers.Domain.Enum;
using System.Collections.Generic;
using System.Linq;

namespace PernikComputers.Service
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext context;

        public ProductService(ApplicationDbContext _context)
        {
            this.context = _context;
        }

        public dynamic GetProduct(string id)
        {
            return context.Products.Find(id);
        }

        public List<Product> GetAllProducts()
        {
            return context.Products.ToList();
        }
        public List<T> GetProducts<T>() where T : class
        {
            return context.Products.OfType<T>().ToList();
        }

        public bool MakeDiscount(string id, int discount)
        {
            Product product = GetProduct(id);

            if (product == null)
            {
                return false;
            }

            product.Discount = product.Price * discount / 100;
            product.Price -= product.Discount;

            context.Products.Update(product);

            return context.SaveChanges() != 0;
        }

        public bool RemoveDiscount(string id)
        {
            Product product = GetProduct(id);

            if (product == null)
            {
                return false;
            }

            product.Price += product.Discount;
            product.Discount = 0;

            context.Products.Update(product);

            return context.SaveChanges() != 0;
        }

        public bool RemoveProduct(string id)
        {
            var item = context.Products.Find(id);

            if (item == null)
            {
                return false;
            }
            context.Products.Remove(item);

            return context.SaveChanges() != 0;
        }
        public List<string> AllDescription(string id)
        {
            var product = context.Products.Find(id);
            List<string> description = null;

            if (product.Category == Category.Processor)
            {
                var x = (Processor)product;
                description = new List<string>()
                {
                    $"Socket: {x.Socket}",
                    $"Operating frequency: {x.CPUSpeed} GHz",
                    $"Turbo Boost: {x.CPUBoostSpeed} GHz",
                    $"Cores: {x.Cores}",
                    $"Threads: {x.Threads}",
                };
            }
            else if (product.Category == Category.Motherboard)
            {
                var x = (Motherboard)product;
                description = new List<string>()
                {
                    $"Socket: {x.Socket}",
                    $"Chipset: {x.Chipset}",
                    $"Supported memory: {x.TypeRam}",
                };
            }
            else if (product.Category == Category.Ram)
            {
                var x = (Ram)product;
                description = new List<string>()
                {
                    $"Capacity: {x.Size} GB",
                    $"Type: {x.TypeRam}",
                    $"Frequency: {x.Frequency} MHz"
                };
            }
            else if (product.Category == Category.VideoCard)
            {
                var x = (VideoCard)product;
                description = new List<string>()
                {
                    $"Graphic Processor: {x.GraphicProcessor}",
                    $"Memory capacity: {x.SizeMemory } GB",
                    $"Memory type: {x.TypeMemory}",
                };
            }
            else if (product.Category == Category.PowerSupply)
            {
                var x = (PowerSupply)product;
                description = new List<string>()
                {
                    $"Power: {x.Power} W",
                    $"Efficiency: {x.Efficiency}%"
                };
            }
            else if (product.Category == Category.Memory)
            {
                var x = (Memory)product;
                description = new List<string>()
                {
                   $"Type: {x.MemoryType} W",
                   $"Capacity: {x.Capacity} GB",
                };
            }
            else if (product.Category == Category.ComputerCase)
            {
                var x = (ComputerCase)product;
                description = new List<string>()
                {
                   $"Type: {x.CaseType} W",
                   $"Form factor: {x.FormFactor}",
                   $"Size: {x.CaseSize} mm",
                };
            }
            else if (product.Category == Category.Computer)
            {
                var x = (Computer)product;
                description = new List<string>()
                {
                   $"{x.Processor.Manufacturer} {x.Processor.Model} ({x.Processor.CPUSpeed}/{x.Processor.CPUBoostSpeed} GHz, {x.Processor.Cache} M)",
                   $"{x.VideoCard.Manufacturer} {x.VideoCard.Model} {x.VideoCard.SizeMemory} GB",
                   $"{x.Ram.Size} GB {x.Ram.TypeRam} {x.Ram.Frequency} MHz"
                };
            }
            return description;
        }
        public List<string> DetailsDescription(string id)
        {
            var product = context.Products.Find(id);
            List<string> description = null;

            if (product.Category == Category.Processor)
            {
                var x = (Processor)product;
                description = new List<string>()
                {
                    $"Socket: {x.Socket}",
                    $"Operating frequency: {x.CPUSpeed} GHz",
                    $"Turbo Boost: {x.CPUBoostSpeed} GHz",
                    $"Cores: {x.Cores}",
                    $"Threads: {x.Threads}",
                    $"Cashe: {x.Cache} MB",
                    $"Warranty: {x.Warranty} months"
                };
            }
            else if (product.Category == Category.Motherboard)
            {
                var x = (Motherboard)product;
                description = new List<string>()
                {
                    $"Socket: {x.Socket}",
                    $"Chipset: {x.Chipset}",
                    $"Supported memory: {x.TypeRam}",
                    $"Number of memory slots: {x.RamSlotsCount}",
                    $"Form factor: {x.FormFactor}",
                    $"Warranty: {x.Warranty} months"
                };
            }
            else if (product.Category == Category.Ram)
            {
                var x = (Ram)product;
                description = new List<string>()
                {
                     $"Capacity: {x.Size} GB",
                    $"Type: {x.TypeRam}",
                    $"Frequency: {x.Frequency} MHz",
                    $"Timing: {x.Timing}",
                    $"Warranty: {x.Warranty} months"
                };
            }
            else if (product.Category == Category.VideoCard)
            {
                var x = (VideoCard)product;
                description = new List<string>()
                {
                    $"Chip manufacturer: {x.ChipManufacturer }",
                    $"Graphic Processor: {x.GraphicProcessor}",
                    $"Memory capacity: {x.SizeMemory } GB",
                    $"Memory type: {x.TypeMemory}",
                    $"Memory Frequency: {x.MemoryFrequency} MHz",
                    $"Core Frequency: {x.CoreFrequency} MHz",
                    $"Current Processes: {x.CurrentProcesses}",
                    $"Rail Width: {x.RailWidth} bit",
                    $"Slot: {x.SlotType}",
                    $"Warranty: {x.Warranty} months"
                };
            }
            else if (product.Category == Category.PowerSupply)
            {
                var x = (PowerSupply)product;
                description = new List<string>()
                {
                    $"Power: {x.Power} W",
                    $"Form factor: {x.FormFactor}",
                    $"Efficiency: {x.Efficiency}%",
                    $"Warranty: {x.Warranty} months"
                };
            }
            else if (product.Category == Category.Memory)
            {
                var x = (Memory)product;
                description = new List<string>()
                {
                   $"Type: {x.MemoryType} W",
                    $"Form factor: {x.FormFactor}",
                    $"Capacity: {x.Capacity} GB",
                    $"Reading speed: {x.ReadSpeed} MB/s",
                    $"Recording speed: {x.WriteSpeed} MB/s",
                    $"Warranty: {x.Warranty} months"
                };
            }
            else if (product.Category == Category.ComputerCase)
            {
                var x = (ComputerCase)product;
                description = new List<string>()
                {
                  $"Type: {x.CaseType} W",
                    $"Form factor: {x.FormFactor}",
                    $"Size: {x.CaseSize} mm",
                    $"Color: {x.Color }",
                    $"Warranty: {x.Warranty} months"
                };
            }
            else if (product.Category == Category.Computer)
            {
                var x = (Computer)product;
                description = new List<string>()
                {
                    $"Processor: {x.Processor.Manufacturer} {x.Processor.Model} ({x.Processor.CPUSpeed}/{x.Processor.CPUBoostSpeed} GHz, {x.Processor.Cache} M, {x.Processor.Cores} cores, {x.Processor.Threads} threads)",
                    $"Video Card: {x.VideoCard.Manufacturer} {x.VideoCard.Model} {x.VideoCard.SizeMemory} GB",
                    $"Ram: {x.Ram.Size} GB {x.Ram.TypeRam} {x.Ram.Frequency} MHz",
                    $"Chipset: {x.Motherboard.Chipset}",
                    $"Motherboard: {x.Motherboard.Manufacturer} {x.Motherboard.Model}",
                    $"Memory: {x.Memory.Capacity} GB {x.Memory.MemoryType}",
                    $"Computer case: {x.ComputerCase.Manufacturer} {x.ComputerCase.Model}",
                    $"Size: {x.ComputerCase.CaseSize} mm",
                    $"Power Supply: {x.PowerSupply.Power} W {x.PowerSupply.Manufacturer} {x.PowerSupply.Model}",
                    $"Warranty: {x.Warranty} months"
                };
            }
            return description;
        }
    }
}
