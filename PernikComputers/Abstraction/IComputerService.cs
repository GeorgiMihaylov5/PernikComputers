using PernikComputers.Domain;
using System.Collections.Generic;

namespace PernikComputers.Abstraction
{
    public interface IComputerService
    {
        public bool Create(string processorId, string motherboardId, string ramId,
            string videoCardId, string powerSupplyId, string memoryId, string computerCaseId,
            string barcode, string manufacturer, string model, int warranty, decimal price, int quantity, string image);
        public bool UpdateComputer(string id, string processorId, string motherboardId, string ramId,
            string videoCardId, string powerSupplyId, string memoryId, string computerCaseId,
            string barcode, string manufacturer, string model, int warranty, decimal price, int quantity, string image);
    }
}
