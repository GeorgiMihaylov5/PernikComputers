using Microsoft.AspNetCore.Mvc;
using PernikComputers.Abstraction;
using PernikComputers.Models;
using System.Collections.Generic;
using System.Linq;

namespace PernikComputers.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService service;

        public ProductsController(IProductService productService)
        {
            this.service = productService;
        }
        public IActionResult All()
        {
            List<ProductAllViewModel> productVm = service.GetProducts()
               .Select(x => new ProductAllViewModel
               {
                   Id = x.Id,
                   Manufacturer = x.Manufacturer,
                   Model = x.Model,
                   Price = x.Price,
                   Discount = x.Discount,
                   Image = x.Image,
                   Category = x.Category,
                   Description = new List<string>()
               }).ToList();

            return View(productVm);
        }
        public IActionResult AllTable()
        {
            List<ProductAllTableViewModel> productVm = service.GetProducts()
               .Select(x => new ProductAllTableViewModel
               {
                   Id = x.Id,
                   Barcode = x.Barcode,
                   Manufacturer = x.Manufacturer,
                   Model = x.Model,
                   Price = x.Price,
                   Image = x.Image,
                   Category = x.Category,
                   Warranty = x.Warranty,
                   Quantity = x.Quantity,
                   Discount = x.Discount
               }).ToList();

            return View(productVm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(string id)
        {
            var isDeleted = service.RemoveProduct(id);
            if (isDeleted)
            {
                return this.RedirectToAction("AllTable");
            }
            return this.RedirectToAction("All", "Employees");
        }
        public IActionResult MakeDiscount(string id)
        {
            var x = service.GetProduct(id);

            ProductPromotionViewModel product = new ProductPromotionViewModel
            {
                Id = x.Id,
                Manufacturer = x.Manufacturer,
                Model = x.Model,
                OldPrice = x.Price,
                NewPrice = x.Price,
                Image = x.Image
            };
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MakeDiscount(string id, int discount)
        {
           service.MakeDiscount(id, discount);

            return this.RedirectToAction("AllTable");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveDiscount(string id, int discount)
        {
            service.RemoveDiscount(id);

            return this.RedirectToAction("AllTable");
        }
        public IActionResult AllDiscounts()
        {
            List<ProductAllViewModel> productVm = service.GetProducts()
              .Where(x => x.Discount != 0)
              .Select(x => new ProductAllViewModel
              {
                  Id = x.Id,
                  Manufacturer = x.Manufacturer,
                  Model = x.Model,
                  Price = x.Price,
                  Discount = x.Discount,
                  Category = x.Category,
                  Image = x.Image,
                  Description = new List<string>()
              }).ToList();

            return View("All", productVm);
        }
    }
}
