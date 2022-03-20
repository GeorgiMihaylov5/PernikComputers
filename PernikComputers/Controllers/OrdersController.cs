using Microsoft.AspNetCore.Mvc;
using PernikComputers.Abstraction;
using PernikComputers.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PernikComputers.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderService service;

        public OrdersController(IOrderService _service)
        {
            this.service = _service;
        }
        public IActionResult All()
        {
            //string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            List<OrderListingViewModel> orders = service.All()
                .Select(x => new OrderListingViewModel
                {
                    Id = x.Id,
                    OrderedOn = x.OrderedOn.ToString("dd-mm,yyyy hh:mm", CultureInfo.InvariantCulture),
                    ProductId = x.ProductId,
                    Model = x.Model,
                    Manufacturer = x.Manufacturer,
                    Category = x.Category.ToString(),
                    ProductPrice = x.Price.ToString(),
                    CustomerId = x.CustomerId,
                    CustomerUsername = x.Customer.UserName,
                    Quantity = x.Count

                }).ToList();

            return View(orders);
        }
        public IActionResult My()
        {
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            List<OrderListingViewModel> orders = service.My(userId)
                .Select(x => new OrderListingViewModel
                {
                    Id = x.Id,
                    OrderedOn = x.OrderedOn.ToString("dd-mm,yyyy hh:mm", CultureInfo.InvariantCulture),
                    ProductId = x.ProductId,
                    Model = x.Model,
                    Manufacturer = x.Manufacturer,
                    Category = x.Category.ToString(),
                    ProductPrice = x.Price.ToString(),
                    CustomerId = x.CustomerId,
                    CustomerUsername = x.Customer.UserName,
                    Quantity = x.Count

                }).ToList();

            return View("All", orders);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(OrderCreateBindingModel viewModel)
        {
            if (ModelState.IsValid)
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var created = service.Create(viewModel.ProductId, viewModel.Count, userId, viewModel.Category, viewModel.Manufacturer, viewModel.Model, decimal.Parse(viewModel.Price));

                if (created)
                {
                    return RedirectToAction("My");
                }
            }
            return RedirectToAction();
        }
    }
}
