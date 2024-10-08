﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PernikComputers.Abstraction;
using PernikComputers.Domain;
using PernikComputers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PernikComputers.Controllers
{
    public class LaptopsController : Controller
    {
        private readonly ILaptopService service;
        private readonly IProductService productService;

        public LaptopsController(ILaptopService _service, IProductService productService)
        {
            this.service = _service;
            this.productService = productService;
        }
        /// <summary>
        /// See all laptops
        /// </summary>
        /// <returns></returns>
        public IActionResult All()
        {
            List<ProductAllViewModel> laptopViewModel = productService.GetProducts<Laptop>()
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

            ViewBag.Manufacturers = laptopViewModel.Select(x => x.Manufacturer).Distinct().ToList();
            ViewBag.Models = laptopViewModel.Select(x => x.Model).Distinct().ToList();

            return View("~/Views/Products/All.cshtml", laptopViewModel);
        }
        /// <summary>
        /// Search laptops by params
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
            var oldProducts = productService.GetProducts<Laptop>();
            List <ProductAllViewModel> laptopViewModel = productService.Search(filter, minPrice, maxPrice, manufacturers, models, oldProducts)
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

            return View("~/Views/Products/All.cshtml", laptopViewModel);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult CreateLaptop()
        {
            return View();
        }
        /// <summary>
        /// Create laptop
        /// </summary>
        /// <param name="createVm"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult CreateLaptop(LaptopCreateViewModel createVm)
        {
            if (ModelState.IsValid)
            {
                var isCreated = service.Create(createVm.Processor, createVm.Motherboard, createVm.Ram,
                    createVm.VideoCard, createVm.PowerSupply, createVm.Memory, createVm.Resolution,
                    createVm.RefreshRate, createVm.DisplaySize, createVm.Color,
                    createVm.Barcode, createVm.Manufacturer, createVm.Model, createVm.Warranty, createVm.Price, createVm.Quantity, createVm.Image);

                if (isCreated)
                {
                    return RedirectToAction("AllTable", "Products");
                }
            }
            return View(createVm);
        }
        /// <summary>
        /// Edit laptop
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Administrator")]
        public IActionResult EditLaptop(string id)
        {
            Laptop item = productService.GetProduct(id);

            if (item == null)
            {
                return NotFound();
            }
            var editModel = new LaptopCreateViewModel()
            {
                Id = item.Id,
                Barcode = item.Barcode,
                Manufacturer = item.Manufacturer,
                Model = item.Model,
                Price = item.Price,
                Warranty = item.Warranty,
                Quantity = item.Quantity,
                Image = item.Image,
                Processor = item.Processor,
                Motherboard = item.Motherboard,
                Ram = item.Ram,
                VideoCard = item.VideoCard,
                PowerSupply = item.PowerSupply,
                Memory = item.Memory,
                Resolution = item.Resolution,
                RefreshRate = item.RefreshRate,
                DisplaySize = item.DisplaySize,
                Color = item.Color
            };

            return View("EditLaptop", editModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult EditLaptop(string id, LaptopCreateViewModel createVm)
        {
            if (ModelState.IsValid)
            {
                var isUpdated = service.Update(createVm.Id, createVm.Processor, createVm.Motherboard, createVm.Ram,
                    createVm.VideoCard, createVm.PowerSupply, createVm.Memory, createVm.Resolution,
                    createVm.RefreshRate, createVm.DisplaySize, createVm.Color,
                    createVm.Barcode, createVm.Manufacturer, createVm.Model, createVm.Warranty, createVm.Price, createVm.Quantity, createVm.Image);

                if (isUpdated)
                {
                    return RedirectToAction("All");
                }
            }
            return View(createVm);
        }
    }
}
