using PernikComputers.Abstraction;
using PernikComputers.Domain;
using PernikComputers.Domain.Enum;
using System.Collections.Generic;

namespace PernikComputers.Service
{
    public class PeripheryService : IPeripheryService
    {
        public bool CreateKeyboard(int keysCount, bool backlight, double cableLength, string size, string barcode, string manufacturer, string model, int warranty, decimal price, int quantity, string image)
        {
            throw new System.NotImplementedException();
        }

        public bool CreateMonitor(int size, string resolution, TypeDisplay typeDisplay, int reactionTime, int refreshRate, string barcode, string manufacturer, string model, int warranty, decimal price, int quantity, string image)
        {
            throw new System.NotImplementedException();
        }

        public bool CreateMouse(int keysCount, bool backlight, double cableLength, string size, int sensitivity, double weight, string barcode, string manufacturer, string model, int warranty, decimal price, int quantity, string image)
        {
            throw new System.NotImplementedException();
        }

        public List<Product> GetPeripheries()
        {
            throw new System.NotImplementedException();
        }

        public bool UpdateKeyboard(string id, int keysCount, bool backlight, double cableLength, string size, string barcode, string manufacturer, string model, int warranty, decimal price, int quantity, string image)
        {
            throw new System.NotImplementedException();
        }

        public bool UpdateMonitor(string id, int size, string resolution, TypeDisplay typeDisplay, int reactionTime, int refreshRate, string barcode, string manufacturer, string model, int warranty, decimal price, int quantity, string image)
        {
            throw new System.NotImplementedException();
        }

        public bool UpdateMouse(string id, int keysCount, bool backlight, double cableLength, string size, int sensitivity, double weight, string barcode, string manufacturer, string model, int warranty, decimal price, int quantity, string image)
        {
            throw new System.NotImplementedException();
        }
    }
}
