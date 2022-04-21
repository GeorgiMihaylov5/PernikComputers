using Microsoft.AspNetCore.Mvc;
using PernikComputers.Abstraction;
using PernikComputers.Models;
using PernikComputers.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace PernikComputers.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class PeripheriesController : Controller
    {
        private readonly IPeripheryService service;
        private readonly IProductService productService;

        public PeripheriesController(IPeripheryService _service, IProductService productService)
        {
            this.service = _service;
            this.productService = productService;
        }
        [AllowAnonymous]
        public IActionResult AllMonitors()
        {
            List<ProductAllViewModel> viewModels = productService.GetProducts<Monitor>()
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

            ViewBag.Manufacturers = viewModels.Select(x => x.Manufacturer).Distinct().ToList();
            ViewBag.Models = viewModels.Select(x => x.Model).ToList();

            return View("~/Views/Products/All.cshtml", viewModels);
        }
        [AllowAnonymous]
        public IActionResult AllKeyboards()
        {
            List<ProductAllViewModel> viewModels = productService.GetProducts<Keyboard>()
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

            ViewBag.Manufacturers = viewModels.Select(x => x.Manufacturer).Distinct().ToList();
            ViewBag.Models = viewModels.Select(x => x.Model).ToList();

            return View("~/Views/Products/All.cshtml", viewModels);
        }
        [AllowAnonymous]
        public IActionResult AllMouses()
        {
            List<ProductAllViewModel> viewModels = productService.GetProducts<Mouse>()
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

            ViewBag.Manufacturers = viewModels.Select(x => x.Manufacturer).Distinct().ToList();
            ViewBag.Models = viewModels.Select(x => x.Model).ToList();

            return View("~/Views/Products/All.cshtml", viewModels);
        }
        public IActionResult CreateMonitor()
        {
            return View();
        }
        public IActionResult CreateKeyboard()
        {
            return View();
        }
        public IActionResult CreateMouse()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateMonitor(MonitorCreateViewModel createVm)
        {
            if (ModelState.IsValid)
            {
                var isCreated = service.CreateMonitor(createVm.Size, createVm.Resolution, createVm.TypeDisplay, createVm.ReactionTime, createVm.RefreshRate,
                    createVm.Barcode, createVm.Manufacturer, createVm.Model, createVm.Warranty, createVm.Price, createVm.Quantity, createVm.Image);

                if (isCreated)
                {
                    return RedirectToAction("AllMonitors");
                }
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateKeyboard(KeyboardCreateViewModel createVm)
        {
            if (ModelState.IsValid)
            {
                var isCreated = service.CreateKeyboard(createVm.KeysCount, createVm.Backlight, createVm.CableLength, createVm.Size,
                    createVm.Barcode, createVm.Manufacturer, createVm.Model, createVm.Warranty, createVm.Price, createVm.Quantity, createVm.Image);

                if (isCreated)
                {
                    return RedirectToAction("AllKeyboards");
                }
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateMouse(MouseCreateViewModel createVm)
        {
            if (ModelState.IsValid)
            {
                var isCreated = service.CreateMouse(createVm.KeysCount, createVm.Backlight, createVm.CableLength, createVm.Size, createVm.Sensitivity, createVm.Weight,
                    createVm.Barcode, createVm.Manufacturer, createVm.Model, createVm.Warranty, createVm.Price, createVm.Quantity, createVm.Image);

                if (isCreated)
                {
                    return RedirectToAction("AllMouses");
                }
            }
            return View();
        }
    }
}
