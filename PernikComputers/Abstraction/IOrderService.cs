using PernikComputers.Domain;
using PernikComputers.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PernikComputers.Abstraction
{
    public interface IOrderService
    {
        public bool Create(string productId, int count, string customerId, Category category, string manufacturer, string model, decimal price);
        public List<Order> All();
        public List<Order> My(string customerId);
        public Order GetOrder(string orderId, string userId);
    }
}
