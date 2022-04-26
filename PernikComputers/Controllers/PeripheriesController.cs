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
        public IActionResult All()
        {
            List<ProductAllViewModel> viewModels = service.GetPeripheries()
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
            ViewBag.Models = viewModels.Select(x => x.Model).Distinct().ToList();

            return View("~/Views/Products/All.cshtml", viewModels);
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult All(string filter, int minPrice, int maxPrice, List<string> manufacturers, List<string> models)
        {
            var oldProducts = service.GetPeripheries();
           List <ProductAllViewModel> viewModels = productService.Search(filter, minPrice, maxPrice, manufacturers, models, oldProducts)
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

            return View("~/Views/Products/All.cshtml", viewModels);
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
        [HttpPost]
        public IActionResult AllMonitors(string filter, int minPrice, int maxPrice, List<string> manufacturers, List<string> models)
        {
            var oldProducts = productService.GetProducts<Monitor>();
            List<ProductAllViewModel> viewModels = productService.Search(filter, minPrice, maxPrice, manufacturers, models, oldProducts)
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
            ViewBag.Models = oldProducts.Select(x => x.Model).ToList();

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
            ViewBag.Models = viewModels.Select(x => x.Model).Distinct().ToList();

            return View("~/Views/Products/All.cshtml", viewModels);
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult AllKeyboards(string filter, int minPrice, int maxPrice, List<string> manufacturers, List<string> models)
        {
            var oldProducts = productService.GetProducts<Keyboard>();
            List <ProductAllViewModel> viewModels = productService.Search(filter, minPrice, maxPrice, manufacturers, models, oldProducts)
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
            ViewBag.Models = viewModels.Select(x => x.Model).Distinct().ToList();

            return View("~/Views/Products/All.cshtml", viewModels);
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult AllMouses(string filter, int minPrice, int maxPrice, List<string> manufacturers, List<string> models)
        {
            var oldProducts = productService.GetProducts<Mouse>();
            List<ProductAllViewModel> viewModels = productService.Search(filter, minPrice, maxPrice, manufacturers, models, oldProducts)
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

        public IActionResult EditMonitor(string id)
        {
            Monitor item = productService.GetProduct(id);

            if (item == null)
            {
                return NotFound();
            }

            var viewModel = new MonitorCreateViewModel()
            {
                Id = item.Id,
                Barcode = item.Barcode,
                Manufacturer = item.Manufacturer,
                Model = item.Model,
                Price = item.Price,
                Warranty = item.Warranty,
                Quantity = item.Quantity,
                Image = item.Image,
                Size = item.Size,
                Resolution = item.Resolution,
                ReactionTime = item.ReactionTime,
                RefreshRate = item.RefreshRate,
                TypeDisplay = item.TypeDisplay,
            };

            return View(viewModel);
        }
        public IActionResult EditKeyboard(string id)
        {
            Keyboard item = productService.GetProduct(id);

            if (item == null)
            {
                return NotFound();
            }

            var viewModel = new KeyboardCreateViewModel()
            {
                Id = item.Id,
                Barcode = item.Barcode,
                Manufacturer = item.Manufacturer,
                Model = item.Model,
                Price = item.Price,
                Warranty = item.Warranty,
                Quantity = item.Quantity,
                Image = item.Image,
                ConnectivityTechnology = item.ConnectivityTechnology,
                KeysCount = item.KeysCount,
                Backlight = item.Backlight,
                CableLength = item.CableLength,
                Size = item.Size,
            };

            return View(viewModel);
        }
        public IActionResult EditMouse(string id)
        {
            Mouse item = productService.GetProduct(id);

            if (item == null)
            {
                return NotFound();
            }

            var viewModel = new MouseCreateViewModel()
            {
                Id = item.Id,
                Barcode = item.Barcode,
                Manufacturer = item.Manufacturer,
                Model = item.Model,
                Price = item.Price,
                Warranty = item.Warranty,
                Quantity = item.Quantity,
                Image = item.Image,
                ConnectivityTechnology = item.ConnectivityTechnology,
                KeysCount = item.KeysCount,
                Backlight = item.Backlight,
                CableLength = item.CableLength,
                Size = item.Size,
                Sensitivity = item.Sensitivity,
                Weight = item.Weight,
            };

            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditMonitor(string id, MonitorCreateViewModel createVm)
        {
            if (ModelState.IsValid)
            {
                var isUpdated = service.UpdateMonitor(createVm.Id, createVm.Size, createVm.Resolution, createVm.TypeDisplay, createVm.ReactionTime, createVm.RefreshRate,
                    createVm.Barcode, createVm.Manufacturer, createVm.Model, createVm.Warranty, createVm.Price, createVm.Quantity, createVm.Image);

                if (isUpdated)
                {
                    return RedirectToAction("AllTable", "Products");
                }
            }
            return View(createVm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditKeyboard(string id, KeyboardCreateViewModel createVm)
        {
            if (ModelState.IsValid)
            {
                var isUpdated = service.UpdateKeyboard(createVm.Id, createVm.KeysCount, createVm.Backlight, createVm.CableLength, createVm.Size,
                    createVm.Barcode, createVm.Manufacturer, createVm.Model, createVm.Warranty, createVm.Price, createVm.Quantity, createVm.Image);

                if (isUpdated)
                {
                    return RedirectToAction("AllTable", "Products");
                }
            }
            return View(createVm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditMouse(string id, Mouse createVm)
        {
            if (ModelState.IsValid)
            {
                var isUpdated = service.UpdateMouse(createVm.Id, createVm.KeysCount, createVm.Backlight, createVm.CableLength, createVm.Size, createVm.Sensitivity, createVm.Weight,
                    createVm.Barcode, createVm.Manufacturer, createVm.Model, createVm.Warranty, createVm.Price, createVm.Quantity, createVm.Image);

                if (isUpdated)
                {
                    return RedirectToAction("AllTable", "Products");
                }
            }
            return View(createVm);
        }
    }
}
