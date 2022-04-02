﻿using Microsoft.EntityFrameworkCore;
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
            return context.Orders.Include(x => x.Customer).Include(x => x.Product).OrderByDescending(x => x.OrderedOn).ToList();
        }
        public List<Order> My(string customerId)
        {
            return context.Orders.Where(x => x.CustomerId == customerId).Include(x => x.Customer).Include(x => x.Product).OrderByDescending(x => x.OrderedOn).ToList();
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

            //if (category == Category.Processor)
            //{
            //    var product = context.Processors.SingleOrDefault(p => p.Id == productId);

            //    if (user == null || product == null || product.Quantity < count)
            //    {
            //        return false;
            //    }

            //    product.Quantity -= count;

            //    context.Processors.Update(product);
            //    context.Orders.Add(order);
            //}
            //else if (category == Category.Motherboard)
            //{
            //    var product = context.Motherboards.SingleOrDefault(p => p.Id == productId);

            //    if (user == null || product == null || product.Quantity < count)
            //    {
            //        return false;
            //    }

            //    product.Quantity -= count;

            //    context.Motherboards.Update(product);
            //    context.Orders.Add(order);
            //}
            //else if (category == Category.Ram)
            //{
            //    var product = context.Rams.SingleOrDefault(p => p.Id == productId);

            //    if (user == null || product == null || product.Quantity < count)
            //    {
            //        return false;
            //    }

            //    product.Quantity -= count;

            //    context.Rams.Update(product);
            //    context.Orders.Add(order);
            //}
            //else if (category == Category.VideoCard)
            //{
            //    var product = context.VideoCards.SingleOrDefault(p => p.Id == productId);

            //    if (user == null || product == null || product.Quantity < count)
            //    {
            //        return false;
            //    }

            //    product.Quantity -= count;

            //    context.VideoCards.Update(product);
            //    context.Orders.Add(order);
            //}
            //else if (category == Category.PowerSupply)
            //{
            //    var product = context.PowerSupplies.SingleOrDefault(p => p.Id == productId);

            //    if (user == null || product == null || product.Quantity < count)
            //    {
            //        return false;
            //    }

            //    product.Quantity -= count;

            //    context.PowerSupplies.Update(product);
            //    context.Orders.Add(order);
            //}
            //else if (category == Category.Memory)
            //{
            //    var product = context.Memories.SingleOrDefault(p => p.Id == productId);

            //    if (user == null || product == null || product.Quantity < count)
            //    {
            //        return false;
            //    }

            //    product.Quantity -= count;

            //    context.Memories.Update(product);
            //    context.Orders.Add(order);
            //}
            //else if (category == Category.ComputerCase)
            //{
            //    var product = context.ComputerCases.SingleOrDefault(p => p.Id == productId);

            //    if (user == null || product == null || product.Quantity < count)
            //    {
            //        return false;
            //    }

            //    product.Quantity -= count;

            //    context.ComputerCases.Update(product);
            //    context.Orders.Add(order);
            //}
            //else if (category == Category.Computer)
            //{
            //    var product = context.Computers.SingleOrDefault(p => p.Id == productId);

            //    if (user == null || product == null || product.Quantity < count)
            //    {
            //        return false;
            //    }

            //    product.Quantity -= count;

            //    context.Computers.Update(product);
            //    context.Orders.Add(order);
            //}

            return context.SaveChanges() != 0;
        }

        public Order GetOrder(string orderId, string userId)
        {
            throw new NotImplementedException();
        }


        //private CommonProperties GetProduct(string productId, Category category)
        //{
        //    var product = new CommonProperties();
        //    if (category == Category.Processor)
        //    {
        //        product = context.Processors.SingleOrDefault(p => p.Id == productId);
        //    }
        //    else if (category == Category.Motherboard)
        //    {
        //        product = context.Motherboards.SingleOrDefault(p => p.Id == productId);
        //    }
        //    else if (category == Category.Ram)
        //    {
        //        product = context.Rams.SingleOrDefault(p => p.Id == productId);
        //    }
        //    else if (category == Category.VideoCard)
        //    {
        //        product = context.VideoCards.SingleOrDefault(p => p.Id == productId);
        //    }
        //    else if (category == Category.PowerSupply)
        //    {
        //        product = context.PowerSupplies.SingleOrDefault(p => p.Id == productId);
        //    }
        //    else if (category == Category.Memory)
        //    {
        //        product = context.Memories.SingleOrDefault(p => p.Id == productId);
        //    }
        //    else if (category == Category.ComputerCase)
        //    {
        //        product = context.ComputerCases.SingleOrDefault(p => p.Id == productId);
        //    }
        //    else if (category == Category.Computer)
        //    {
        //        product = context.Computers.SingleOrDefault(p => p.Id == productId);
        //    }

        //    return product;
        //}
    }
}
