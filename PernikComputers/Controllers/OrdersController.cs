using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PernikComputers.Abstraction;
using PernikComputers.Domain.Enum;
using PernikComputers.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PernikComputers.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrderService service;

        public OrdersController(IOrderService _service)
        {
            this.service = _service;
        }
        /// <summary>
        /// View all orders
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Administrator,Employee")]
        public IActionResult All()
        {
            List<OrderListingViewModel> orders = service.All()
                .Select(x => new OrderListingViewModel
                {
                    Id = x.Id,
                    OrderedOn = x.OrderedOn.ToString("dd-MMM,yyyy hh:mm tt", CultureInfo.InvariantCulture),
                    ProductId = x.ProductId,
                    Model = x.Product.Model,
                    Manufacturer = x.Product.Manufacturer,
                    Category = x.Product.Category.ToString(),
                    ProductPrice = x.OrderedPrice.ToString(),
                    CustomerId = x.CustomerId,
                    CustomerUsername = x.Customer.UserName,
                    Quantity = x.Count,
                    Status = x.Status.ToString(),
                    TotalPrice = (x.Count * x.OrderedPrice).ToString()

                }).OrderByDescending(x => x.Status == OrderStatus.Pending.ToString())
                .ThenByDescending(x => x.Status == OrderStatus.Approved.ToString())
                .ThenByDescending(x => x.Status == OrderStatus.Completed.ToString()).ToList();

            return View(orders);
        }
        /// <summary>
        /// View orders for current user
        /// </summary>
        /// <returns></returns>
        public IActionResult My()
        {
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            List<OrderListingViewModel> orders = service.My(userId)
                .Select(x => new OrderListingViewModel
                {
                    Id = x.Id,
                    OrderedOn = x.OrderedOn.ToString("dd-MMM,yyyy hh:mm tt", CultureInfo.InvariantCulture),
                    ProductId = x.ProductId,
                    Model = x.Product.Model,
                    Manufacturer = x.Product.Manufacturer,
                    Category = x.Product.Category.ToString(),
                    ProductPrice = x.OrderedPrice.ToString(),
                    CustomerId = x.CustomerId,
                    CustomerUsername = x.Customer.UserName,
                    Quantity = x.Count,
                    Status = x.Status.ToString(),
                    TotalPrice = (x.Count * x.OrderedPrice).ToString()

                }).OrderByDescending(x => x.Status == OrderStatus.Pending.ToString())
                .ThenByDescending(x => x.Status == OrderStatus.Approved.ToString())
                .ThenByDescending(x => x.Status == OrderStatus.Completed.ToString()).ToList();

            return View("All", orders);
        }
        /// <summary>
        /// Create order
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
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
        /// <summary>
        /// View rejected orders
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Administrator,Employee")]
        public IActionResult Rejected()
        {
            List<OrderListingViewModel> orders = service.All().Where(x => x.Status == OrderStatus.Rejected)
                .Select(x => new OrderListingViewModel
                {
                    Id = x.Id,
                    OrderedOn = x.OrderedOn.ToString("dd-MMM,yyyy hh:mm tt", CultureInfo.InvariantCulture),
                    ProductId = x.ProductId,
                    Model = x.Product.Model,
                    Manufacturer = x.Product.Manufacturer,
                    Category = x.Product.Category.ToString(),
                    ProductPrice = x.OrderedPrice.ToString(),
                    CustomerId = x.CustomerId,
                    CustomerUsername = x.Customer.UserName,
                    Quantity = x.Count,
                    Status = x.Status.ToString(),
                    TotalPrice = (x.Count * x.Product.Price).ToString()

                }).ToList();
            return View("All", orders);
        }
        /// <summary>
        /// See details of order
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        public IActionResult Details(string id)
        {
            var order = service.GetOrder(id);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var detailsViewModel = new OrderDetailsViewModel()
            {
                Id = order.Id,
                OrderedOn = order.OrderedOn.ToString("dd-MMM,yyyy hh:mm tt", CultureInfo.InvariantCulture),
                ProductId = order.ProductId,
                Manufacturer = order.Product.Manufacturer,
                Model = order.Product.Model,
                Category = order.Product.Category.ToString(),
                Image = order.Product.Image,
                Warranty = order.Product.Warranty.ToString(),
                Barcode = order.Product.Barcode,
                OrderedPrice = (order.OrderedPrice * order.Count).ToString(),
                SinglePrice = order.OrderedPrice.ToString(),
                CustomerId = order.CustomerId,
                CustomerUsername = order.Customer.UserName,
                Quantity = order.Count,
                Status = order.Status.ToString(),
                Notes = order.Notes
            };

            return View(detailsViewModel);
        }
        /// <summary>
        /// Edit order
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Administrator,Employee")]
        public IActionResult Edit(string id)
        {
            var order = service.GetOrder(id);

            var viewModel = new OrderDetailsViewModel()
            {
                Id = order.Id,
                OrderedOn = order.OrderedOn.ToString("dd-MMM,yyyy hh:mm tt", CultureInfo.InvariantCulture),
                ProductId = order.ProductId,
                Manufacturer = order.Product.Manufacturer,
                Model = order.Product.Model,
                Category = order.Product.Category.ToString(),
                Image = order.Product.Image,
                Warranty = order.Product.Warranty.ToString(),
                Barcode = order.Product.Barcode,
                OrderedPrice = (order.OrderedPrice * order.Count).ToString(),
                SinglePrice = order.OrderedPrice.ToString(),
                CustomerId = order.CustomerId,
                CustomerUsername = order.Customer.UserName,
                Quantity = order.Count,
                Status = order.Status.ToString(),
                Notes = order.Notes
            };

            return View(viewModel);
        }
        [Authorize(Roles = "Administrator,Employee")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(OrderDetailsViewModel editViewModel)
        {
            var status = (OrderStatus)Enum.Parse(typeof(OrderStatus), editViewModel.Status);
            var isUpdated = service.Update(editViewModel.Id, status, editViewModel.Notes);

            if (isUpdated)
            {
                return RedirectToAction("All");
            }
            return View();
        }
    }
}
