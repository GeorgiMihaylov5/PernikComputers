using PernikComputers.Abstraction;
using PernikComputers.Data;
using PernikComputers.Domain;
using PernikComputers.Domain.Enum;
using System.Collections.Generic;
using System.Linq;

namespace PernikComputers.Service
{
    public class PeripheryService : IPeripheryService
    {
        private readonly ApplicationDbContext context;

        public PeripheryService(ApplicationDbContext _context)
        {
            this.context = _context;
        }
        public bool CreateKeyboard(int keysCount, bool backlight, double cableLength, string size, string barcode, string manufacturer, string model, int warranty, decimal price, int quantity, string image)
        {
            var item = new Keyboard()
            {
                KeysCount = keysCount,
                Backlight = backlight,
                CableLength = cableLength,
                Size =size,
                Barcode = barcode,
                Manufacturer = manufacturer,
                Model = model,
                Category = Category.Keyboard,
                Price = price,
                Warranty = warranty,
                Quantity = quantity,
                Image = image
            };

            context.Keyboards.Add(item);
            return context.SaveChanges() != 0;
        }

        public bool CreateMonitor(double size, string resolution, TypeDisplay typeDisplay, int reactionTime, int refreshRate, string barcode, string manufacturer, string model, int warranty, decimal price, int quantity, string image)
        {
            var item = new Monitor()
            {
                Size = size,
                Resolution = resolution,
                TypeDisplay = typeDisplay,
                ReactionTime = reactionTime,
                RefreshRate = refreshRate,
                Barcode = barcode,
                Manufacturer = manufacturer,
                Model = model,
                Category = Category.Monitor,
                Price = price,
                Warranty = warranty,
                Quantity = quantity,
                Image = image
            };

            context.Monitors.Add(item);
            return context.SaveChanges() != 0;
        }

        public bool CreateMouse(int keysCount, bool backlight, double cableLength, string size, int sensitivity, double weight, string barcode, string manufacturer, string model, int warranty, decimal price, int quantity, string image)
        {
            var item = new Mouse()
            {
                KeysCount = keysCount,
                Backlight = backlight,
                CableLength = cableLength,
                Size = size,
                Sensitivity = sensitivity,
                Weight = weight,
                Barcode = barcode,
                Manufacturer = manufacturer,
                Model = model,
                Category = Category.Mouse,
                Price = price,
                Warranty = warranty,
                Quantity = quantity,
                Image = image
            };

            context.Mouses.Add(item);
            return context.SaveChanges() != 0;
        }

        public List<Product> GetPeripheries()
        {
            return context.Products
                .Where(x => (x.Category == Category.Monitor || x.Category == Category.Keyboard || x.Category == Category.Mouse) && x.IsRemoved != true)
                .ToList();
        }

        public bool UpdateKeyboard(string id, int keysCount, bool backlight, double cableLength, string size, string barcode, string manufacturer, string model, int warranty, decimal price, int quantity, string image)
        {
            var item = context.Keyboards.Find(id);

            if (item == null)
            {
                return false;
            }

            item.KeysCount = keysCount;
            item.Backlight = backlight;
            item.CableLength = cableLength;
            item.Size = size;
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

        public bool UpdateMonitor(string id, double size, string resolution, TypeDisplay typeDisplay, int reactionTime, int refreshRate, string barcode, string manufacturer, string model, int warranty, decimal price, int quantity, string image)
        {
            var item = context.Monitors.Find(id);

            if (item == null)
            {
                return false;
            }

            item.Size = size;
            item.Resolution = resolution;
            item.TypeDisplay = typeDisplay;
            item.ReactionTime = reactionTime;
            item.RefreshRate = refreshRate;
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

        public bool UpdateMouse(string id, int keysCount, bool backlight, double cableLength, string size, int sensitivity, double weight, string barcode, string manufacturer, string model, int warranty, decimal price, int quantity, string image)
        {
            var item = context.Mouses.Find(id);

            if (item == null)
            {
                return false;
            }

            item.KeysCount = keysCount;
            item.Backlight = backlight;
            item.CableLength = cableLength;
            item.Size = size;
            item.Sensitivity = sensitivity;
            item.Weight = weight;
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
