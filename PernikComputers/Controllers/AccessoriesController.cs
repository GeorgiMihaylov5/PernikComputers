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
    public class AccessoriesController : Controller
    {

        private readonly IAccessoryService service;
        private readonly IProductService productService;

        public AccessoriesController(IAccessoryService _service, IProductService productService)
        {
            this.service = _service;
            this.productService = productService;
        }
        /// <summary>
        /// View all accessories
        /// </summary>
        /// <returns></returns>
        public IActionResult All()
        {
            List<ProductAllViewModel> viewModel = productService.GetProducts<Accessory>()
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

            ViewBag.Manufacturers = viewModel.Select(x => x.Manufacturer).Distinct().ToList();
            ViewBag.Models = viewModel.Select(x => x.Model).Distinct().ToList();

            return View("~/Views/Products/All.cshtml", viewModel);
        }
        /// <summary>
        /// Search accessories by params
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
            var oldProducts = productService.GetProducts<Accessory>();
            List<ProductAllViewModel> viewModel = productService.Search(filter, minPrice, maxPrice, manufacturers, models, oldProducts)
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

            return View("~/Views/Products/All.cshtml", viewModel);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult CreateAccessory()
        {
            return View();
        }

        /// <summary>
        /// Create accessory
        /// </summary>
        /// <param name="createVm"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult CreateAccessory(AccessoryCreateViewModel createVm)
        {
            if (ModelState.IsValid)
            {
                var isCreated = service.CreateAccessory(createVm.TypeAccessory, createVm.Description,
                    createVm.Barcode, createVm.Manufacturer, createVm.Model, createVm.Warranty, createVm.Price, createVm.Quantity, createVm.Image);

                if (isCreated)
                {
                    return RedirectToAction("AllTable", "Products");
                }
            }
            return View(createVm);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult EditAccessory(string id)
        {
            Accessory item = productService.GetProduct(id);

            if (item == null)
            {
                return NotFound();
            }
            var editModel = new AccessoryCreateViewModel()
            {
                Id = item.Id,
                Barcode = item.Barcode,
                Manufacturer = item.Manufacturer,
                Model = item.Model,
                Price = item.Price,
                Warranty = item.Warranty,
                Quantity = item.Quantity,
                Image = item.Image,
                TypeAccessory = item.TypeAccessory,
                Description = item.Description
            };

            return View(editModel);

        }
        /// <summary>
        /// Edit accessory
        /// </summary>
        /// <param name="id"></param>
        /// <param name="createVm"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult EditAccessory(string id, AccessoryCreateViewModel createVm)
        {
            if (ModelState.IsValid)
            {
                var isUpdated = service.UpdateAccessory(createVm.Id, createVm.TypeAccessory, createVm.Description,
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
