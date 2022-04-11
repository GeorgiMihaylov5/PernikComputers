using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PernikComputers.Abstraction;
using PernikComputers.Domain;
using PernikComputers.Domain.Enum;
using PernikComputers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PernikComputers.Controllers
{
    public class ComputersController : Controller
    {
        private readonly IComputerService service;
        private readonly IProductService productService;

        public ComputersController(IComputerService _service, IProductService productService)
        {
            this.service = _service;
            this.productService = productService;
        }

        public IActionResult All()
        {
            List<ProductAllViewModel> processorVM = productService.GetProducts<Computer>()
                .Select(x => new ProductAllViewModel
                {
                    Id = x.Id,
                    Manufacturer = x.Manufacturer,
                    Model = x.Model,
                    Price = x.Price,
                    Discount = x.Discount,
                    Image = x.Image,
                    Category = x.Category,
                    Description = productService.AllDescription(x.Id),
                    Quantity = x.Quantity
                }).ToList();


            return View("~/Views/Products/All.cshtml", processorVM);
            //return RedirectToPage("All","Components", processorVM);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            ViewBag.Processors = productService.GetProducts<Processor>();
            ViewBag.Motherboards = productService.GetProducts<Motherboard>();
            ViewBag.Rams = productService.GetProducts<Ram>();
            ViewData["ProcessorId"] = new SelectList(productService.GetProducts<Processor>(), "Id", "Model");
            ViewData["MotherboardId"] = new SelectList(productService.GetProducts<Motherboard>(), "Id", "Model");
            ViewData["RamId"] = new SelectList(productService.GetProducts<Ram>(), "Id", "Model");
            ViewData["VideoCardId"] = new SelectList(productService.GetProducts<VideoCard>(), "Id", "Model");
            ViewData["PowerSupplyId"] = new SelectList(productService.GetProducts<PowerSupply>(), "Id", "Model");
            ViewData["MemoryId"] = new SelectList(productService.GetProducts<Memory>(), "Id", "Model");
            ViewData["ComputerCaseId"] = new SelectList(productService.GetProducts<ComputerCase>(), "Id", "Model");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult Create(ComputerCreateViewModel createVm)
        {
            try
            {
                var isCreated = service.Create(createVm.ProcessorId, createVm.MotherboardId, createVm.RamId,
                    createVm.VideoCardId, createVm.PowerSupplyId, createVm.MemoryId, createVm.ComputerCaseId,
                    createVm.Barcode, createVm.Manufacturer, createVm.Model, createVm.Warranty, createVm.Quantity);

                if (isCreated)
                {
                    return RedirectToAction("AllTable", "Products");
                }
                return View(createVm);
            }
            catch
            {
                return View(createVm);
            }
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(string id)
        {
            Computer item = productService.GetProduct(id);

            if (item == null)
            {
                return NotFound();
            }
            var editModel = new ComputerCreateViewModel()
            {
                Id = item.Id,
                Barcode = item.Barcode,
                Manufacturer = item.Manufacturer,
                Model = item.Model,
                Price = item.Price,
                Warranty = item.Warranty,
                Quantity = item.Quantity,
                Image = item.Image,
                ProcessorId = item.ProcessorId,
                MotherboardId = item.MotherboardId,
                RamId = item.RamId,
                VideoCardId = item.VideoCardId,
                PowerSupplyId = item.PowerSupplyId,
                MemoryId = item.MemoryId,
                ComputerCaseId = item.ComputerCaseId
            };

            return View("Create", editModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(string id, ComputerCreateViewModel createVm)
        {
            if (ModelState.IsValid)
            {
                var isUpdated = service.UpdateComputer(createVm.Id, createVm.ProcessorId, createVm.MotherboardId, createVm.RamId,
                    createVm.VideoCardId, createVm.PowerSupplyId, createVm.MemoryId, createVm.ComputerCaseId,
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
