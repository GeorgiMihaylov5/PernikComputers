using PernikComputers.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PernikComputers.Abstraction
{
    public interface ICreatableComponents
    {
        public bool CreateProcessor(string socket, double cpuSpeed, double cpuBoostSpeed, int cores, int threads, int cache,
            string barcode, string manufacturer, string model, int warranty, decimal price, int quantity, string image);
        public bool CreateMotherboard(string socket, string chipset, TypeRam typeRam, int ramSlotsCount, string formFactor,
            string barcode, string manufacturer, string model, int warranty, decimal price, int quantity, string image);
        public bool CreateRam(int size, TypeRam typeRam, int frequency, string timing,
            string barcode, string manufacturer, string model, int warranty, decimal price, int quantity, string image);
        public bool CreateVideoCard(ChipManufacturer chipManufacturer, string graphicProcessor, int sizeMemory, string typeMemory,
            int memoryFrequency, int coreFrequency, int currentProcesses, int railWidth, string slotType,
           string barcode, string manufacturer, string model, int warranty, decimal price, int quantity, string image);
        public bool CreatePowerSupply(int power, string formFactor, int efficiency,
            string barcode, string manufacturer, string model, int warranty, decimal price, int quantity, string image);
        public bool CreateMemory(MemoryType memoryType, string formFactor, int capacity, int readSpeed, int writeSpeed,
           string barcode, string manufacturer, string model, int warranty, decimal price, int quantity, string image);
        public bool CreateComputerCase(string caseType, string formFactor, string caseSize, string color,
            string barcode, string manufacturer, string model, int warranty, decimal price, int quantity, string image);
    }
}
