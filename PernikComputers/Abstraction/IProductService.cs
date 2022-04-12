using PernikComputers.Domain;
using System.Collections.Generic;

namespace PernikComputers.Abstraction
{
    public interface IProductService
    {
        public List<Product> GetAllProducts();
        public List<T> GetProducts<T>() where T : class;
        public dynamic GetProduct(string id);
        public bool RemoveProduct(string id);
        public bool MakeDiscount(string id, int discount);
        public bool RemoveDiscount(string id);
    }
}
