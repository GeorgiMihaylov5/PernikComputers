using PernikComputers.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PernikComputers.Abstraction
{
    public interface IUpdatableComponents
    {
        public bool UpdateProcessor(string id, string socket, double cpuSpeed, double cpuBoostSpeed, int cores, int threads, int cache,
            string barcode, string manufacturer, string model, int warranty, decimal price, int quantity, string image);
        public bool UpdateMotherboard(string id, string socket, string chipset, TypeRam typeRam, int ramSlotsCount, string formFactor,
            string barcode, string manufacturer, string model, int warranty, decimal price, int quantity, string image);
        public bool UpdateRam(string id, int size, TypeRam typeRam, int frequency, string timing,
            string barcode, string manufacturer, string model, int warranty, decimal price, int quantity, string image);
        public bool UpdateVideoCard(string id, ChipManufacturer chipManufacturer, string graphicProcessor, int sizeMemory, string typeMemory,
            int memoryFrequency, int coreFrequency, int currentProcesses, int railWidth, string slotType,
           string barcode, string manufacturer, string model, int warranty, decimal price, int quantity, string image);
        public bool UpdatePowerSupply(string id, int power, string formFactor, int efficiency,
            string barcode, string manufacturer, string model, int warranty, decimal price, int quantity, string image);
        public bool UpdateMemory(string id, MemoryType memoryType, string formFactor, int capacity, int readSpeed, int writeSpeed,
           string barcode, string manufacturer, string model, int warranty, decimal price, int quantity, string image);
        public bool UpdateComputerCase(string id, string caseType, string formFactor, string caseSize, string color,
            string barcode, string manufacturer, string model, int warranty, decimal price, int quantity, string image);
    }
}
