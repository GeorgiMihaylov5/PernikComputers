using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PernikComputers.Abstraction
{
    public interface ILaptopService
    {
        public bool Create(string processor, string motherboard, string ram, string videoCard, string powerSupply, 
            string memory, string resolution, int refreshRate, double displaySize, string color,
            string barcode, string manufacturer, string model, int warranty, decimal price, int quantity, string image);

        public bool Update(string id, string processor, string motherboard, string ram, string videoCard, string powerSupply,
           string memory, string resolution, int refreshRate, double displaySize, string color,
           string barcode, string manufacturer, string model, int warranty, decimal price, int quantity, string image);
    }
}
