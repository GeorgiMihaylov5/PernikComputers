using PernikComputers.Abstraction;
using PernikComputers.Data;
using PernikComputers.Domain;
using PernikComputers.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PernikComputers.Service
{
    public class LaptopService : ILaptopService
    {
        private readonly ApplicationDbContext context;

        public LaptopService(ApplicationDbContext _context)
        {
            this.context = _context;
        }
        public bool Create(string processor, string motherboard, string ram, string videoCard, string powerSupply, string memory, string resolution, int refreshRate, double displaySize, string color, string barcode, string manufacturer, string model, int warranty, decimal price, int quantity, string image)
        {
            var item = new Laptop()
            {
                Processor = processor,
                Motherboard = motherboard,
                Ram = ram,
                VideoCard = videoCard,
                PowerSupply = powerSupply,
                Memory = memory,
                Resolution = resolution,
                RefreshRate = refreshRate,
                DisplaySize = displaySize,
                Color = color,
                Barcode = barcode,
                Manufacturer = manufacturer,
                Model = model,
                Category = Category.Laptop,
                Price = price,
                Warranty = warranty,
                Quantity = quantity,
                Image = image
            };

            context.Laptops.Add(item);
            return context.SaveChanges() != 0;
        }

        public bool Update(string id, string processor, string motherboard, string ram, string videoCard, string powerSupply, string memory, string resolution, int refreshRate, double displaySize, string color, string barcode, string manufacturer, string model, int warranty, decimal price, int quantity, string image)
        {
            var item = context.Laptops.Find(id);

            if (item == null)
            {
                return false;
            }
            item.Processor = processor;
            item.Motherboard = motherboard;
            item.Ram = ram;
            item.VideoCard = videoCard;
            item.PowerSupply = powerSupply;
            item.Memory = memory;
            item.Resolution = resolution;
            item.RefreshRate = refreshRate;
            item.DisplaySize = displaySize;
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
