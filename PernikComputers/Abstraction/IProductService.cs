using PernikComputers.Domain;
using System.Collections.Generic;

namespace PernikComputers.Abstraction
{
    public interface IProductService
    {
        public List<CommonProperties> GetProducts();
    }
}
