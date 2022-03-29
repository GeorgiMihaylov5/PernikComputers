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
                   Promotion = x.Promotion,
                   Image = x.Image,
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
                   Promotion = x.Promotion
               }).ToList();

            return View(productVm);
        }
    }
}
