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
        /// <summary>
        /// View all products
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Search products by params
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="minPrice"></param>
        /// <param name="maxPrice"></param>
        /// <param name="manufacturers"></param>
        /// <param name="models"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult All(string filter, int minPrice, int maxPrice, List<string> manufacturers, List<string> models)
        {
            var oldProducts = service.GetAllProducts();
            List<ProductAllViewModel> productVm = service.Search(filter, minPrice, maxPrice, manufacturers, models, oldProducts)
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

            return View(productVm);
        }
        /// <summary>
        /// View all products for control panel
        /// </summary>
        /// <returns></returns>
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
               }).OrderBy(x => x.Category).ToList();

            return View(productVm);
        }
        /// <summary>
        /// Remove product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Make discount by percentages
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Remove discount
        /// </summary>
        /// <param name="id"></param>
        /// <param name="discount"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult RemoveDiscount(string id, int discount)
        {
            service.RemoveDiscount(id);

            return this.RedirectToAction("AllTable");
        }
        /// <summary>
        /// View all products on discount
        /// </summary>
        /// <returns></returns>
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
        [HttpPost]
        public IActionResult AllDiscounts(string filter, int minPrice, int maxPrice, List<string> manufacturers, List<string> models)
        {
            var oldProducts = service.GetAllProducts().Where(x => x.Discount != 0).ToList();
            List<ProductAllViewModel> productVm = service.Search(filter, minPrice, maxPrice, manufacturers, models, oldProducts)
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

            ViewBag.Manufacturers = oldProducts.Select(x => x.Manufacturer).Distinct().ToList();
            ViewBag.Models = oldProducts.Select(x => x.Model).Distinct().ToList();

            return View("All", productVm);
        }
        /// <summary>
        /// Products details. In this view the user can order a product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
                Category = x.Category,
                Price = x.Price,
                Quantity = x.Quantity,
                Image = x.Image,
            };

            return View("~/Views/Products/Details.cshtml", detailsViewModel);
        }
        
    }
}
