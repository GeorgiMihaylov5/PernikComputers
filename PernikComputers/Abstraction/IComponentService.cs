﻿using PernikComputers.Domain;
using PernikComputers.Domain.Enum;
using System.Collections.Generic;

namespace PernikComputers.Abstraction
{
    public interface IComponentService
    {
        public bool CreateProcessor(string socket, double cpuSpeed, double cpuBoostSpeed, int cores, int threads, int cache, 
            string barcode, string manufacturer, string model, int warranty, decimal price, int quantity, string image);
        public bool CreateMotherboard(string socket, string chipset, TypeRam typeRam, int ramSlotsCount, string formFactor,
            string barcode, string manufacturer, string model, int warranty, decimal price, int quantity, string image);
        public bool CreateRam(int size, TypeRam typeRam, int frequency, string timing,
            string barcode, string manufacturer, string model, int warranty, decimal price, int quantity, string image);
        public bool CreateVideoCard(ChipManufacturer chipManufacturer, string graphicProcessor, int sizeMemory, string typeMemory,
            int memoryFrequency, int coreFrequency, int currentProcessors, int railWidth, string slotType,
           string barcode, string manufacturer, string model, int warranty, decimal price, int quantity, string image);
        public bool CreatePowerSupply(int power, string formFactor, int efficiency,
            string barcode, string manufacturer, string model, int warranty, decimal price, int quantity, string image);
        public bool CreateMemory(MemoryType memoryType, string formFactor, int capacity, int readSpeed, int writeSpeed,
           string barcode, string manufacturer, string model, int warranty, decimal price, int quantity, string image);
        public bool CreateComputerCase(string caseType, string formFactor, string caseSize, string color,
            string barcode, string manufacturer, string model, int warranty, decimal price, int quantity, string image);
        public List<Processor> GetProcessors();
        public List<Motherboard> GetMotherboards();
        public List<Ram> GetRams();
        public List<VideoCard> GetVideoCards();
        public List<PowerSupply> GetPowerSupplies();
        public List<Memory> GetMemories();
        public List<ComputerCase> GetComputerCases();

        public Processor GetProcessor(string id);
        public Motherboard GetMotherboard(string id);
        public Ram GetRam(string id);
        public VideoCard GetVideoCard(string id);
        public PowerSupply GetPowerSupply(string id);
        public Memory GetMemory(string id);
        public ComputerCase GetComputerCase(string id);
    }
}