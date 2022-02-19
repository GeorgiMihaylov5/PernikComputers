using PernikComputers.Domain;
using PernikComputers.Domain.Enum;
using System.Collections.Generic;

namespace PernikComputers.Abstraction
{
    public interface IComponentsService
    {
        public bool CreateProcessor(string socket, double cpuSpeed, double cpuBoostSpeed, int cores, int threads, int cache, 
            string barcode, string name, string manufacturer, string model, int warranty, decimal price, int quantity, string image);
        public bool CreateMotherboard(string socket, string chipset, TypeRam typeRam, int ramSlotsCount, string formFactor,
            string barcode, string name, string manufacturer, string model, int warranty, decimal price, int quantity, string image);

        public bool CreateRam(int size, TypeRam typeRam, int frequency, string timing,
            string barcode, string name, string manufacturer, string model, int warranty, decimal price, int quantity, string image);

        public List<Processor> GetProcessors();
        public List<Motherboard> GetMotherboards();
        public List<Ram> GetRams();
    }
}
