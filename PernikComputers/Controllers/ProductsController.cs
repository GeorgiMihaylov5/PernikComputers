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
                   IsPromotion = x.IsPromotion,
                   Image = x.Image,
               }).ToList();

            return View(productVm);
        }
    }
}
