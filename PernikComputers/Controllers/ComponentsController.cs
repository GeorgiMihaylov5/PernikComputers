﻿using Microsoft.AspNetCore.Mvc;
using PernikComputers.Abstraction;
using PernikComputers.Models;
using System.Collections.Generic;
using System.Linq;

namespace PernikComputers.Controllers
{
    public class ComponentsController : Controller
    {
        private readonly IComponentsService service;

        public ComponentsController(IComponentsService _service)
        {
            this.service = _service;
        }
        //--------Processors---------
        public IActionResult AllProcessors()
        {
            List<ProductAllViewModel> processorVM = service.GetProcessors()
                .Select(x => new ProductAllViewModel
                {
                    Id = x.Id,
                    Manufacturer = x.Manufacturer,
                    Model = x.Model,
                    Price = x.Price,
                    IsPromotion = x.IsPromotion,
                    Image = x.Image
                }).ToList();

            
            return View("All", processorVM);
        }
        public IActionResult CreateProcessor()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateProcessor(ProcessorCreateViewModel createVm)
        {
            if (ModelState.IsValid)
            {
                var isCreated = service.CreateProcessor(createVm.Socket, createVm.CPUSpeed, createVm.CPUBoostSpeed, createVm.Cores, createVm.Threads, createVm.Cache,
                    createVm.Barcode, createVm.Manufacturer, createVm.Model, createVm.Warranty, createVm.Price, createVm.Quantity, createVm.Image);

                if (isCreated)
                {
                    return RedirectToAction("AllProcessors");
                }
            }
            return View();
        }


        //-------Motherboards-------------

        public IActionResult AllMotherboards()
        {
            List<ProductAllViewModel> motherboardVM = service.GetMotherboards()
                .Select(x => new ProductAllViewModel
                {
                    Id = x.Id,
                    Manufacturer = x.Manufacturer,
                    Model = x.Model,
                    Price = x.Price,
                    IsPromotion = x.IsPromotion,
                    Image = x.Image
                }).ToList();


            return View("All", motherboardVM);
        }

        public IActionResult CreateMotherboard()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateMotherboard(MotherboardCreateViewModel createVm)
        {
            if (ModelState.IsValid)
            {
                var isCreated = service.CreateMotherboard(createVm.Socket, createVm.Chipset, createVm.TypeRam, createVm.RamSlotsCount, createVm.FormFactor,
                    createVm.Barcode, createVm.Manufacturer, createVm.Model, createVm.Warranty, createVm.Price, createVm.Quantity, createVm.Image);

                if (isCreated)
                {
                    return RedirectToAction("AllMotherboards");
                }
            }
            return View();
        }

        //--------Ram---------
        public IActionResult AllRams()
        {
            List<ProductAllViewModel> ramVM = service.GetRams()
                .Select(x => new ProductAllViewModel
                {
                    Id = x.Id,
                    Manufacturer = x.Manufacturer,
                    Model = x.Model,
                    Price = x.Price,
                    IsPromotion = x.IsPromotion,
                    Image = x.Image
                }).ToList();


            return View("All", ramVM);
        }
        public IActionResult CreateRam()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateRam(RamCreateViewModel createVm)
        {
            if (ModelState.IsValid)
            {
                var isCreated = service.CreateRam(createVm.Size, createVm.TypeRam, createVm.Frequency, createVm.Timing,
                    createVm.Barcode, createVm.Manufacturer, createVm.Model, createVm.Warranty, createVm.Price, createVm.Quantity, createVm.Image);

                if (isCreated)
                {
                    return RedirectToAction("AllRams");
                }
            }
            return View();
        }

        //-------VideoCard-------------

        public IActionResult AllVideoCards()
        {
            List<ProductAllViewModel> motherboardVM = service.GetMotherboards()
                .Select(x => new ProductAllViewModel
                {
                    Id = x.Id,
                    Manufacturer = x.Manufacturer,
                    Model = x.Model,
                    Price = x.Price,
                    IsPromotion = x.IsPromotion,
                    Image = x.Image
                }).ToList();


            return View("All", motherboardVM);
        }

        public IActionResult CreateVideoCard()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateVideoCard(MotherboardCreateViewModel createVm)
        {
            if (ModelState.IsValid)
            {
                var isCreated = service.CreateMotherboard(createVm.Socket, createVm.Chipset, createVm.TypeRam, createVm.RamSlotsCount, createVm.FormFactor,
                    createVm.Barcode, createVm.Manufacturer, createVm.Model, createVm.Warranty, createVm.Price, createVm.Quantity, createVm.Image);

                if (isCreated)
                {
                    return RedirectToAction("AllMotherboards");
                }
            }
            return View();
        }
    }
}