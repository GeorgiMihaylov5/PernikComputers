using Microsoft.EntityFrameworkCore;
using PernikComputers.Abstraction;
using PernikComputers.Data;
using PernikComputers.Domain;
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

        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            products.AddRange(context.Processors);
            products.AddRange(context.Motherboards);
            products.AddRange(context.Rams);
            products.AddRange(context.VideoCards);
            products.AddRange(context.PowerSupplies);
            products.AddRange(context.Memories);
            products.AddRange(context.ComputerCases);
            products.AddRange(context.Computers);

            return products;
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
    }
}
