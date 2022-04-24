using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PernikComputers.Abstraction;
using PernikComputers.Domain;
using PernikComputers.Domain.Enum;
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
            List<ProductAllViewModel> productVm = service.GetAllProducts()
               .Select(x => new ProductAllViewModel
               {
                   Id = x.Id,
                   Manufacturer = x.Manufacturer,
                   Model = x.Model,
                   Price = x.Price,
                   Discount = x.Discount,
                   Image = x.Image,
                   Category = x.Category,
                   Description =  x.PartialDescription,
                   Quantity = x.Quantity
               }).ToList();

            ViewBag.Manufacturers = productVm.Select(x => x.Manufacturer).Distinct().ToList();
            ViewBag.Models = productVm.Select(x => x.Model).Distinct().ToList();

            return View(productVm);
        }
        [Authorize(Roles = "Administrator")]
        public IActionResult AllTable()
        {
            List<ProductAllTableViewModel> productVm = service.GetAllProducts()
               .Select(x => new ProductAllTableViewModel
               {
                   Id = x.Id,
                   Barcode = x.Barcode,
                   Manufacturer = x.Manufacturer,
                   Model = x.Model,
                   Price = x.Price,
                   Image = x.Image,
                   Category = x.Category.ToString(),
                   Warranty = x.Warranty,
                   Quantity = x.Quantity,
                   Discount = x.Discount
               }).ToList();

            return View(productVm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult Delete(string id)
        {
            try
            {
                var isDeleted = service.RemoveProduct(id);
                if (isDeleted)
                {
                    return this.RedirectToAction("AllTable");
                }
                return this.RedirectToAction("All", "Employees");
            }
            catch
            {
                return this.RedirectToAction("All", "Employees");
            }
            
        }
        [Authorize(Roles = "Administrator")]
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
        [Authorize(Roles = "Administrator")]
        public IActionResult MakeDiscount(string id, int discount)
        {
           service.MakeDiscount(id, discount);

            return this.RedirectToAction("AllTable");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult RemoveDiscount(string id, int discount)
        {
            service.RemoveDiscount(id);

            return this.RedirectToAction("AllTable");
        }
        public IActionResult AllDiscounts()
        {
            List<ProductAllViewModel> productVm = service.GetAllProducts()
              .Where(x => x.Discount != 0)
              .Select(x => new ProductAllViewModel
              {
                  Id = x.Id,
                  Manufacturer = x.Manufacturer,
                  Model = x.Model,
                  Price = x.Price,
                  Discount = x.Discount,
                  Image = x.Image,
                  Category = x.Category,
                  Description = x.PartialDescription,
                  Quantity = x.Quantity
              }).ToList();

            ViewBag.Manufacturers = productVm.Select(x => x.Manufacturer).Distinct().ToList();
            ViewBag.Models = productVm.Select(x => x.Model).Distinct().ToList();

            return View("All", productVm);
        }
        [AllowAnonymous]
        public IActionResult Details(string id)
        {
            Product x = service.GetProduct(id);

            if (x == null)
            {
                return NoContent();
            }

            ProductDetailsViewModel detailsViewModel = new ProductDetailsViewModel
            {
                Id = x.Id,
                Barcode = x.Barcode,
                Model = x.Model,
                Manufacturer = x.Manufacturer,
                Discount = x.Discount,
                Description = x.FullDescription.ToList(),
                Category = Category.Processor,
                Price = x.Price,
                Quantity = x.Quantity,
                Image = x.Image,
            };

            return View("~/Views/Products/Details.cshtml", detailsViewModel);
        }
        [AllowAnonymous]
        public IActionResult Search(string filter, int minPrice, int maxPrice, List<string> manufacturers, List<string> models)
        {
            var products = new List<Product>();

            if (manufacturers.Count > 0)
            {
                foreach (var item in manufacturers)
                {
                    var currentProducts = service.GetAllProducts().Where(x => x.Manufacturer == item).ToList();
                    products.AddRange(currentProducts);
                }
            }
            else
            {
                products = service.GetAllProducts();
            }
            if (models.Count > 0)
            {
                foreach (var item in models)
                {
                    products = products.Where(x => x.Model == item).Distinct().ToList();
                }
            }

            if (minPrice > 0 && maxPrice > minPrice)
            {
                products = products.Where(x => x.Price >= minPrice && x.Price <= maxPrice).ToList();
            }
            else if (minPrice > 0 && maxPrice <= minPrice)
            {
                products = products.Where(x => x.Price >= minPrice).ToList();
            }
            else if (maxPrice > 0)
            {
                products = products.Where(x => x.Price <= maxPrice).ToList();
            }

            if (filter == "1")
            {
                products = products.Where(x => x.Discount != 0).ToList();
            }
            else if (filter == "2")
            {
                products = products.OrderByDescending(x => x.Orders.Count).ToList();
            }
            else if (filter == "3")
            {
                products = products.OrderBy(x => x.Price).ToList();
            }

            List<ProductAllViewModel> productVm = products
              .Select(x => new ProductAllViewModel
              {
                  Id = x.Id,
                  Manufacturer = x.Manufacturer,
                  Model = x.Model,
                  Price = x.Price,
                  Discount = x.Discount,
                  Image = x.Image,
                  Category = x.Category,
                  Description = x.PartialDescription,
                  Quantity = x.Quantity
              }).ToList();

            ViewBag.Manufacturers = service.GetAllProducts().Select(x => x.Manufacturer).Distinct().ToList();
            ViewBag.Models = service.GetAllProducts().Select(x => x.Model).Distinct().ToList();
            return View("All", productVm);
        }
    }
}
