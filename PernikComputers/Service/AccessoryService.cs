using PernikComputers.Abstraction;
using PernikComputers.Data;
using PernikComputers.Domain;
using PernikComputers.Domain.Enum;
using System.Collections.Generic;
using System.Linq;

namespace PernikComputers.Service
{
    public class AccessoryService : IAccessoryService
    {
        private readonly ApplicationDbContext context;

        public AccessoryService(ApplicationDbContext _context)
        {
            this.context = _context;
        }
        public bool CreateAccessory(string typeAccessory, string descripton, string barcode, string manufacturer, string model, int warranty, decimal price, int quantity, string image)
        {
            var accessory = new Accessory()
            {
                TypeAccessory = typeAccessory,
                Description = descripton,
                Barcode = barcode,
                Manufacturer = manufacturer,
                Model = model,
                Category = Category.Accessories,
                Price = price,
                Warranty = warranty,
                Quantity = quantity,
                Image = image
            };

            context.Accessories.Add(accessory);

            return context.SaveChanges() != 0;
        }

        public List<Product> GetAccessories()
        {
            return context.Products.Where(x => x.Category == Category.Accessories).ToList();
        }

        public bool UpdateAccessory(string id, string typeAccessory, string descripton, string barcode, string manufacturer, string model, int warranty, decimal price, int quantity, string image)
        {
            var item = context.Accessories.Find(id);

            if (item == null)
            {
                return false;
            }
            item.TypeAccessory = typeAccessory;
            item.Description = descripton;
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
