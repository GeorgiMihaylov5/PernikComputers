using PernikComputers.Domain;
using System.Collections.Generic;

namespace PernikComputers.Abstraction
{
    public interface IAccessoryService
    {
        public List<Product> GetAccessories();
        public bool CreateAccessory(string typeAccessory, string descripton,
            string barcode, string manufacturer, string model, int warranty, decimal price, int quantity, string image);
        public bool UpdateAccessory(string id, string typeAccessory, string descripton,
            string barcode, string manufacturer, string model, int warranty, decimal price, int quantity, string image);
    }
}
