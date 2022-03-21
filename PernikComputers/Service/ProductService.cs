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
        public List<CommonProperties> GetProducts()
        {
            List<CommonProperties> products = new List<CommonProperties>();
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
    }
}
