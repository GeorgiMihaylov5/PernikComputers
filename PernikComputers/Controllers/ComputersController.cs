﻿using Microsoft.AspNetCore.Authorization;
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

        /// <summary>
        /// View all computers
        /// </summary>
        /// <returns></returns>
        public IActionResult All()
        {
            List<ProductAllViewModel> computerViewModel = productService.GetProducts<Computer>()
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

            ViewBag.Manufacturers = computerViewModel.Select(x => x.Manufacturer).Distinct().ToList();
            ViewBag.Models = computerViewModel.Select(x => x.Model).Distinct().ToList();

            return View("~/Views/Products/All.cshtml", computerViewModel);
        }
        /// <summary>
        /// Search computers by params
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
            var oldProducts = productService.GetProducts<Computer>();
            List <ProductAllViewModel> computerViewModel = productService.Search(filter, minPrice, maxPrice, manufacturers, models, oldProducts)
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

            return View("~/Views/Products/All.cshtml", computerViewModel);
        }

        /// <summary>
        /// Create computer. When the computer is set up, the components quantity decreases
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Administrator")]
        public IActionResult CreateComputer()
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
        public IActionResult CreateComputer(ComputerCreateViewModel createVm)
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
        public IActionResult EditComputer(string id)
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

            return View("EditComputer", editModel);

        }

        /// <summary>
        /// Edit computer. The number of computers quantity can only increase
        /// </summary>
        /// <param name="id"></param>
        /// <param name="createVm"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult EditComputer(string id, ComputerCreateViewModel createVm)
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult Restore(ComputerCreateViewModel viewModel)
        {
            Computer computer = productService.GetProduct(viewModel.Id);
            computer.Price = computer.Processor.Price + computer.Motherboard.Price +
                computer.Ram.Price + computer.VideoCard.Price +
                computer.PowerSupply.Price + computer.Memory.Price +
                computer.ComputerCase.Price;
            computer.Image = computer.ComputerCase.Image;

            return View("EditComputer", computer);
        }
        //private int ComputerQuantity(Computer computer)
        //{
        //    int[] quantities =
        //    {
        //        computer.Processor.Quantity,
        //        computer.Motherboard.Quantity,
        //        computer.Ram.Quantity,
        //        computer.VideoCard.Quantity,
        //        computer.PowerSupply.Quantity,
        //        computer.Memory.Quantity,
        //        computer.ComputerCase.Quantity
        //    };
        //    return quantities.Min();
        //}
    }
}
