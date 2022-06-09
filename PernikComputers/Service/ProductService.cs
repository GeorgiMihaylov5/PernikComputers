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
            return context.Products.Where(x => x.IsRemoved != true).FirstOrDefault(x => x.Id == id);
        }

        public List<Product> GetAllProducts()
        {
            return context.Products.Where(x => x.IsRemoved != true).ToList();
        }
        public List<T> GetProducts<T>() where T : class
        {
            return context.Products.Where(x => x.IsRemoved != true).OfType<T>().ToList();
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
            item.IsRemoved = true;
            context.Products.Update(item);

            return context.SaveChanges() != 0;
        }
        public List<Product> Search(string filter, int minPrice, int maxPrice, ICollection<string> manufacturers, ICollection<string> models, IEnumerable<Product> oldProducts)
        {
            var products = new List<Product>();

            if (manufacturers.Count > 0)
            {
                foreach (var item in manufacturers)
                {
                    var currentProducts = oldProducts.Where(x => x.Manufacturer == item).ToList();
                    products.AddRange(currentProducts);
                }
            }
            else
            {
                products = oldProducts.ToList();
            }
            if (models.Count > 0)
            {
                foreach (var item in models)
                {
                    products = products.Where(x => x.Model == item).Distinct().ToList();
                }
            }

            if (minPrice > 0 && maxPrice > minPrice)
            {
                products = products.Where(x => x.Price >= minPrice && x.Price <= maxPrice).ToList();
            }
            else if (minPrice == maxPrice && minPrice > 0)
            {
                products = products.Where(x => x.Price == minPrice).ToList();
            }
            else if (minPrice > 0 && maxPrice < minPrice)
            {
                products = products.Where(x => x.Price >= minPrice).ToList();
            }
            else if (maxPrice > 0)
            {
                products = products.Where(x => x.Price <= maxPrice).ToList();
            }

            if (filter == "1")
            {
                products = products.Where(x => x.Discount != 0).ToList();
            }
            else if (filter == "2")
            {
                products = products.OrderByDescending(x => x.Orders.Count).ToList();
            }
            else if (filter == "3")
            {
                products = products.OrderBy(x => x.Price).ToList();
            }

            return products;
        }
    }
}
