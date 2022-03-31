using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PernikComputers.Abstraction;
using PernikComputers.Data;
using PernikComputers.Domain;
using PernikComputers.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PernikComputers.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IComputerService service;
        ApplicationDbContext context;

        public HomeController(ILogger<HomeController> logger, IComputerService service, ApplicationDbContext context)
        {
            _logger = logger;
            this.service = service;
            this.context = context;
        }

        public IActionResult Index()
        {
            Dictionary<string, int> map = new Dictionary<string, int>();

            foreach (var order in context.Orders.ToList())
            {
                if (!map.ContainsKey(order.ProductId))
                {
                    map.Add(order.ProductId, 1);
                }
                else
                {
                    map[order.ProductId]++;
                }
            }
            var ids = map.OrderByDescending(x => x.Value).Take(6).Select(x => x.Key).ToList();
            var products = context.Products.Where(x => ids.Contains(x.Id)).ToList();

            List<ProductAllViewModel> productVm = products
               .Select(x => new ProductAllViewModel
               {
                   Id = x.Id,
                   Manufacturer = x.Manufacturer,
                   Model = x.Model,
                   Price = x.Price,
                   Image = x.Image,
                   Category = x.Category,
                   Discount = x.Discount,
                   Description = new List<string>()
               }).ToList();

            return View(productVm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
