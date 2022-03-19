using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PernikComputers.Abstraction;
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
        private readonly IComponentService componentService;

        public ComputersController(IComputerService _service, IComponentService _componentService)
        {
            this.service = _service;
            this.componentService = _componentService;
        }

        public IActionResult All()
        {
            List<ProductAllViewModel> processorVM = service.GetComputers()
                .Select(x => new ProductAllViewModel
                {
                    Id = x.Id,
                    Manufacturer = x.Manufacturer,
                    Model = x.Model,
                    Price = x.Price,
                    IsPromotion = x.IsPromotion,
                    Image = x.Image,
                    Description = new List<string>()
                    {
                        $"{x.Processor.Manufacturer} {x.Processor.Model} ({x.Processor.CPUSpeed}/{x.Processor.CPUBoostSpeed} GHz, {x.Processor.Cache} M)",
                        $"{x.VideoCard.Manufacturer} {x.VideoCard.Model} {x.VideoCard.SizeMemory} GB",
                        $"{x.Ram.Size} GB {x.Ram.TypeRam} {x.Ram.Frequency} MHz"
                    },
                    DetailsAction = "Details"
                }).ToList();


            return View("All", processorVM);
            //return RedirectToPage("All","Components", processorVM);
        }

        public IActionResult Details(string id)
        {
            var x = service.GetComputer(id);

            ProductDetailsViewModel detailsViewModel = new ProductDetailsViewModel
            {
                Id = x.Id,
                Barcode = x.Barcode,
                Model = x.Model,
                Manufacturer = x.Manufacturer,
                IsPromotion = x.IsPromotion,
                Category = Category.Processor,
                Description = new List<string>()
                {
                    $"Processor: {x.Processor.Manufacturer} {x.Processor.Model} ({x.Processor.CPUSpeed}/{x.Processor.CPUBoostSpeed} GHz, {x.Processor.Cache} M, {x.Processor.Cores} cores, {x.Processor.Threads} threads)",
                    $"Video Card: {x.VideoCard.Manufacturer} {x.VideoCard.Model} {x.VideoCard.SizeMemory} GB",
                    $"Ram: {x.Ram.Size} GB {x.Ram.TypeRam} {x.Ram.Frequency} MHz",
                    $"Chipset: {x.Motherboard.Chipset}",
                    $"Motherboard: {x.Motherboard.Manufacturer} {x.Motherboard.Model}",
                    $"Memory: {x.Memory.Capacity} GB {x.Memory.MemoryType}",
                    $"Computer case: {x.ComputerCase.Manufacturer} {x.ComputerCase.Model}",
                    $"Size: {x.ComputerCase.CaseSize} mm",
                    $"Power Supply: {x.PowerSupply.Power} W {x.PowerSupply.Manufacturer} {x.PowerSupply.Model}",
                    $"Warranty: {x.Warranty} months"
                },
                Price = x.Price,
                Quantity = x.Quantity,
                Image = x.Image,
            };

            return View("Details", detailsViewModel);
        }

        public IActionResult Create()
        {
            ViewData["ProcessorId"] = new SelectList(componentService.GetProcessors(), "Id", "Model");
            ViewData["MotherboardId"] = new SelectList(componentService.GetMotherboards(), "Id", "Model");
            ViewData["RamId"] = new SelectList(componentService.GetRams(), "Id", "Model");
            ViewData["VideoCardId"] = new SelectList(componentService.GetVideoCards(), "Id", "Model");
            ViewData["PowerSupplyId"] = new SelectList(componentService.GetPowerSupplies(), "Id", "Model");
            ViewData["MemoryId"] = new SelectList(componentService.GetMemories(), "Id", "Model");
            ViewData["ComputerCaseId"] = new SelectList(componentService.GetComputerCases(), "Id", "Model");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ComputerCreateViewModel createVm)
        {
            if (ModelState.IsValid)
            {
                var isCreated = service.Create(createVm.ProcessorId, createVm.MotherboardId, createVm.RamId,
                    createVm.VideoCardId, createVm.PowerSupplyId, createVm.MemoryId, createVm.ComputerCaseId,
                    createVm.Barcode, createVm.Manufacturer, createVm.Model, createVm.Warranty, createVm.Price, createVm.Quantity, createVm.Image);

                if (isCreated)
                {
                    return RedirectToAction("All");
                }
            }
            return View();
            //ViewData["GanreId"] = new SelectList(_context.Ganres, "Id", "Name", movie.GanreId);
        }

        public IActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public IActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
