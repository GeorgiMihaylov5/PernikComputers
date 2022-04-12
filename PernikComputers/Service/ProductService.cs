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
    }
}
