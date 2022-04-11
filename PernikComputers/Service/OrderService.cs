using Microsoft.EntityFrameworkCore;
using PernikComputers.Abstraction;
using PernikComputers.Data;
using PernikComputers.Domain;
using PernikComputers.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PernikComputers.Service
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext context;

        public OrderService(ApplicationDbContext _context)
        {
            this.context = _context;
        }

        public List<Order> All()
        {
            return context.Orders.OrderByDescending(x => x.OrderedOn).ToList();
        }
        public List<Order> My(string customerId)
        {
            return context.Orders.Where(x => x.CustomerId == customerId).OrderByDescending(x => x.OrderedOn).ToList();
        }

        public bool Create(string productId, int count, string customerId, Category category, string manufacturer, string model, decimal price)
        {
            var user = context.Users.SingleOrDefault(s => s.Id == customerId);

            Order order = new Order
            {
                OrderedOn = DateTime.Now,
                ProductId = productId,
                Count = count,
                OrderedPrice = price,
                CustomerId = customerId,
                Category = category,
                Status = OrderStatus.Pending,
            };

            var product = context.Products.SingleOrDefault(p => p.Id == productId);

            if (user == null || product == null || product.Quantity < count)
            {
                return false;
            }

            product.Quantity -= count;

            context.Products.Update(product);
            context.Orders.Add(order);

            return context.SaveChanges() != 0;
        }

        public Order GetOrder(string orderId)
        {
            return context.Orders.Find(orderId);
        }

        public bool Update(string id, OrderStatus orderStatus, string notes)
        {
            throw new NotImplementedException();
        }
    }
}
