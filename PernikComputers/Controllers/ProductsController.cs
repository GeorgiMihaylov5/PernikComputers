﻿using Microsoft.AspNetCore.Authorization;
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
                   Description =  service.AllDescription(x.Id)
               }).ToList();

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
                   Category = x.Category,
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
                  Description = service.AllDescription(x.Id)
              }).ToList();

            return View("All", productVm);
        }
        [AllowAnonymous]
        public IActionResult Details(string id)
        {
            Product x = service.GetProduct(id);

            ProductDetailsViewModel detailsViewModel = new ProductDetailsViewModel
            {
                Id = x.Id,
                Barcode = x.Barcode,
                Model = x.Model,
                Manufacturer = x.Manufacturer,
                Discount = x.Discount,
                Description = service.DetailsDescription(x.Id),
                Category = Category.Processor,
                Price = x.Price,
                Quantity = x.Quantity,
                Image = x.Image,
            };

            return View("~/Views/Products/Details.cshtml", detailsViewModel);
        }
    }
}
