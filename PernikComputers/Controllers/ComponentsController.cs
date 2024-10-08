﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PernikComputers.Abstraction;
using PernikComputers.Domain;
using PernikComputers.Domain.Enum;
using PernikComputers.Models;
using System.Collections.Generic;
using System.Linq;

namespace PernikComputers.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ComponentsController : Controller
    {
        private readonly IComponentService service;
        private readonly IProductService productService;

        public ComponentsController(IComponentService _service, IProductService productService)
        {
            this.service = _service;
            this.productService = productService;
        }
        /// <summary>
        /// See all components
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public IActionResult All()
        {
            List<ProductAllViewModel> componentVM = service.GetComponents()
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


            ViewBag.Manufacturers = componentVM.Select(x => x.Manufacturer).Distinct().ToList();
            ViewBag.Models = componentVM.Select(x => x.Model).Distinct().ToList();

            return View("~/Views/Products/All.cshtml", componentVM);
        }
        /// <summary>
        /// Search components by params
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="minPrice"></param>
        /// <param name="maxPrice"></param>
        /// <param name="manufacturers"></param>
        /// <param name="models"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult All(string filter, int minPrice, int maxPrice, List<string> manufacturers, List<string> models)
        {
            var oldProducts = service.GetComponents();
            List<ProductAllViewModel> componentVM = productService.Search(filter, minPrice, maxPrice, manufacturers, models, oldProducts)
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

            return View("~/Views/Products/All.cshtml", componentVM);
        }
            //--------Processors---------
        [AllowAnonymous]
        public IActionResult AllProcessors()
        {
            List<ProductAllViewModel> processorVM = productService.GetProducts<Processor>()
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


            ViewBag.Manufacturers = processorVM.Select(x => x.Manufacturer).Distinct().ToList();
            ViewBag.Models = processorVM.Select(x => x.Model).Distinct().ToList();

            return View("~/Views/Products/All.cshtml", processorVM);
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult AllProcessors(string filter, int minPrice, int maxPrice, List<string> manufacturers, List<string> models)
        {
            var oldProducts = productService.GetProducts<Processor>();
            List<ProductAllViewModel> processorVM = productService.Search(filter, minPrice, maxPrice, manufacturers, models, oldProducts)
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

            return View("~/Views/Products/All.cshtml", processorVM);
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
                var isCreated = service.CreateProcessor(createVm.Socket.Replace(" ",""), createVm.CPUSpeed, createVm.CPUBoostSpeed, createVm.Cores, createVm.Threads, createVm.Cache,
                    createVm.Barcode, createVm.Manufacturer, createVm.Model, createVm.Warranty, createVm.Price, createVm.Quantity, createVm.Image);

                if (isCreated)
                {
                    return RedirectToAction("AllProcessors");
                }
            }
            return View();
        }

        public IActionResult EditProcessor(string id)
        {
            Processor item = productService.GetProduct(id);

            if (item == null)
            {
                return NotFound();
            }
            var editModel = new ProcessorCreateViewModel
            {
                Id = item.Id,
                Socket = item.Socket,
                CPUSpeed = item.CPUSpeed,
                CPUBoostSpeed = item.CPUBoostSpeed,
                Cores = item.Cores,
                Threads = item.Threads,
                Cache = item.Cache,
                Barcode = item.Barcode,
                Manufacturer = item.Manufacturer,
                Model = item.Model,
                Price = item.Price,
                Warranty = item.Warranty,
                Quantity = item.Quantity,
                Image = item.Image
            };

            return View("EditProcessor", editModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditProcessor(string id, ProcessorCreateViewModel createVm)
        {
            if (ModelState.IsValid)
            {
                var isUpdated = service.UpdateProcessor(createVm.Id, createVm.Socket, createVm.CPUSpeed, createVm.CPUBoostSpeed, createVm.Cores, createVm.Threads, createVm.Cache,
                    createVm.Barcode, createVm.Manufacturer, createVm.Model, createVm.Warranty, createVm.Price, createVm.Quantity, createVm.Image);

                if (isUpdated)
                {
                    return RedirectToAction("AllTable","Products");
                }
            }
            return View(createVm);
        }

        //-------Motherboards-------------
        [AllowAnonymous]
        public IActionResult AllMotherboards()
        {
            List<ProductAllViewModel> motherboardVM = productService.GetProducts<Motherboard>()
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

            ViewBag.Manufacturers = motherboardVM.Select(x => x.Manufacturer).Distinct().ToList();
            ViewBag.Models = motherboardVM.Select(x => x.Model).Distinct().ToList();

            return View("~/Views/Products/All.cshtml", motherboardVM);
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult AllMotherboards(string filter, int minPrice, int maxPrice, List<string> manufacturers, List<string> models)
        {
            var oldProducts = productService.GetProducts<Motherboard>();
            List<ProductAllViewModel> motherboardVM = productService.Search(filter, minPrice, maxPrice, manufacturers, models, oldProducts)
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

            return View("~/Views/Products/All.cshtml", motherboardVM);
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
                var isCreated = service.CreateMotherboard(createVm.Socket.Replace(" ", ""), createVm.Chipset, createVm.TypeRam, createVm.RamSlotsCount, createVm.FormFactor,
                    createVm.Barcode, createVm.Manufacturer, createVm.Model, createVm.Warranty, createVm.Price, createVm.Quantity, createVm.Image);

                if (isCreated)
                {
                    return RedirectToAction("AllMotherboards");
                }
            }
            return View();
        }
        
        public IActionResult EditMotherboard(string id)
        {
            Motherboard item = productService.GetProduct(id);

            if (item == null)
            {
                return NotFound();
            }
            var editModel = new MotherboardCreateViewModel
            {
                Id = item.Id,
                Socket = item.Socket,
                Chipset = item.Chipset,
                TypeRam = item.TypeRam,
                RamSlotsCount = item.RamSlotsCount,
                FormFactor = item.FormFactor,
                Barcode = item.Barcode,
                Manufacturer = item.Manufacturer,
                Model = item.Model,
                Price = item.Price,
                Warranty = item.Warranty,
                Quantity = item.Quantity,
                Image = item.Image
            };

            return View(editModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditMotherboard(string id, MotherboardCreateViewModel createVm)
        {
            if (ModelState.IsValid)
            {
                var isUpdated = service.UpdateMotherboard(createVm.Id, createVm.Socket, createVm.Chipset, createVm.TypeRam, createVm.RamSlotsCount, createVm.FormFactor,
                    createVm.Barcode, createVm.Manufacturer, createVm.Model, createVm.Warranty, createVm.Price, createVm.Quantity, createVm.Image);

                if (isUpdated)
                {
                    return RedirectToAction("AllMotherboards");
                }
            }
            return View(createVm);
        }

        //--------Ram---------
        [AllowAnonymous]
        public IActionResult AllRams()
        {
            List<ProductAllViewModel> ramVM = productService.GetProducts<Ram>()
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

            ViewBag.Manufacturers = ramVM.Select(x => x.Manufacturer).Distinct().ToList();
            ViewBag.Models = ramVM.Select(x => x.Model).Distinct().ToList();

            return View("~/Views/Products/All.cshtml", ramVM);
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult AllRams(string filter, int minPrice, int maxPrice, List<string> manufacturers, List<string> models)
        {
           var oldProducts = productService.GetProducts<Ram>();
           List <ProductAllViewModel> ramVM = productService.Search(filter, minPrice, maxPrice, manufacturers, models, oldProducts)
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

            return View("~/Views/Products/All.cshtml", ramVM);
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
        public IActionResult EditRam(string id)
        {
            Ram item = productService.GetProduct(id);

            if (item == null)
            {
                return NotFound();
            }
            var editModel = new RamCreateViewModel
            {
                Id = item.Id,
                Size = item.Size,
                TypeRam = item.TypeRam,
                Frequency = item.Frequency,
                Timing = item.Timing,
                Barcode = item.Barcode,
                Manufacturer = item.Manufacturer,
                Model = item.Model,
                Price = item.Price,
                Warranty = item.Warranty,
                Quantity = item.Quantity,
                Image = item.Image
            };

            return View(editModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditRam(string id, RamCreateViewModel createVm)
        {
            if (ModelState.IsValid)
            {
                var isUpdated = service.UpdateRam(createVm.Id, createVm.Size, createVm.TypeRam, createVm.Frequency, createVm.Timing,
                    createVm.Barcode, createVm.Manufacturer, createVm.Model, createVm.Warranty, createVm.Price, createVm.Quantity, createVm.Image);

                if (isUpdated)
                {
                    return RedirectToAction("AllRams");
                }
            }
            return View(createVm);
        }

        //-------VideoCard-------------

        [AllowAnonymous]
        public IActionResult AllVideoCards()
        {
            List<ProductAllViewModel> videoCardVM = productService.GetProducts<VideoCard>()
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

            ViewBag.Manufacturers = videoCardVM.Select(x => x.Manufacturer).Distinct().ToList();
            ViewBag.Models = videoCardVM.Select(x => x.Model).Distinct().ToList();

            return View("~/Views/Products/All.cshtml", videoCardVM);
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult AllVideoCards(string filter, int minPrice, int maxPrice, List<string> manufacturers, List<string> models)
        {
            var oldProducts = productService.GetProducts<VideoCard>();
            List <ProductAllViewModel> videoCardVM = productService.Search(filter, minPrice, maxPrice, manufacturers, models, oldProducts)
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

            return View("~/Views/Products/All.cshtml", videoCardVM);
        }

        public IActionResult CreateVideoCard()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateVideoCard(VideoCardCreateViewModel createVm)
        {
            if (ModelState.IsValid)
            {
                var isCreated = service.CreateVideoCard(createVm.ChipManufacturer, createVm.GraphicProcessor, createVm.SizeMemory, createVm.TypeMemory, 
                    createVm.MemoryFrequency, createVm.CoreFrequency, createVm.CurrentProcesses, createVm.RailWidth, createVm.SlotType, 
                    createVm.Barcode, createVm.Manufacturer, createVm.Model, createVm.Warranty, createVm.Price, createVm.Quantity, createVm.Image);

                if (isCreated)
                {
                    return RedirectToAction("AllVideoCards");
                }
            }
            return View();
        }
        public IActionResult EditVideoCard(string id)
        {
            VideoCard item = productService.GetProduct(id);

            if (item == null)
            {
                return NotFound();
            }
            var editModel = new VideoCardCreateViewModel
            {
                Id = item.Id,
                ChipManufacturer = item.ChipManufacturer,
                GraphicProcessor = item.GraphicProcessor,
                SizeMemory = item.SizeMemory,
                TypeMemory = item.TypeMemory,
                MemoryFrequency = item.MemoryFrequency,
                CoreFrequency = item.CoreFrequency,
                CurrentProcesses = item.CurrentProcesses,
                RailWidth = item.RailWidth,
                SlotType = item.SlotType,
                Barcode = item.Barcode,
                Manufacturer = item.Manufacturer,
                Model = item.Model,
                Price = item.Price,
                Warranty = item.Warranty,
                Quantity = item.Quantity,
                Image = item.Image
            };

            return View(editModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditVideoCard(string id, VideoCardCreateViewModel createVm)
        {
            if (ModelState.IsValid)
            {
                var isUpdated = service.UpdateVideoCard(createVm.Id, createVm.ChipManufacturer, createVm.GraphicProcessor, createVm.SizeMemory, createVm.TypeMemory,
                    createVm.MemoryFrequency, createVm.CoreFrequency, createVm.CurrentProcesses, createVm.RailWidth, createVm.SlotType,
                    createVm.Barcode, createVm.Manufacturer, createVm.Model, createVm.Warranty, createVm.Price, createVm.Quantity, createVm.Image);

                if (isUpdated)
                {
                    return RedirectToAction("AllVideoCards");
                }
            }
            return View(createVm);
        }

        //-------PowerSupply-------------
        [AllowAnonymous]
        public IActionResult AllPowerSupplies()
        {
            List<ProductAllViewModel> powerSupplyVM = productService.GetProducts<PowerSupply>()
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

            ViewBag.Manufacturers = powerSupplyVM.Select(x => x.Manufacturer).Distinct().ToList();
            ViewBag.Models = powerSupplyVM.Select(x => x.Model).Distinct().ToList();

            return View("~/Views/Products/All.cshtml", powerSupplyVM);
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult AllPowerSupplies(string filter, int minPrice, int maxPrice, List<string> manufacturers, List<string> models)
        {
            var oldProducts = productService.GetProducts<PowerSupply>();
            List <ProductAllViewModel> powerSupplyVM = productService.Search(filter, minPrice, maxPrice, manufacturers, models, oldProducts)
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

            return View("~/Views/Products/All.cshtml", powerSupplyVM);
        }

        public IActionResult CreatePowerSupply()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePowerSupply(PowerSupplyCreateViewModel createVm)
        {
            if (ModelState.IsValid)
            {
                var isCreated = service.CreatePowerSupply(createVm.Power, createVm.FormFactor, createVm.Efficiency,
                    createVm.Barcode, createVm.Manufacturer, createVm.Model, createVm.Warranty, createVm.Price, createVm.Quantity, createVm.Image);

                if (isCreated)
                {
                    return RedirectToAction("AllPowerSupplies");
                }
            }
            return View();
        }
        public IActionResult EditPowerSupply(string id)
        {
            PowerSupply item = productService.GetProduct(id);

            if (item == null)
            {
                return NotFound();
            }
            var editModel = new PowerSupplyCreateViewModel
            {
                Id = item.Id,
                Power = item.Power,
                FormFactor = item.FormFactor,
                Efficiency = item.Efficiency,
                Barcode = item.Barcode,
                Manufacturer = item.Manufacturer,
                Model = item.Model,
                Price = item.Price,
                Warranty = item.Warranty,
                Quantity = item.Quantity,
                Image = item.Image
            };

            return View(editModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditPowerSupply(string id, PowerSupplyCreateViewModel createVm)
        {
            if (ModelState.IsValid)
            {
                var isUpdated = service.UpdatePowerSupply(createVm.Id, createVm.Power, createVm.FormFactor, createVm.Efficiency,
                    createVm.Barcode, createVm.Manufacturer, createVm.Model, createVm.Warranty, createVm.Price, createVm.Quantity, createVm.Image);

                if (isUpdated)
                {
                    return RedirectToAction("AllPowerSupplies");
                }
            }
            return View(createVm);
        }

        //-------Memory-------------
        [AllowAnonymous]
        public IActionResult AllMemories()
        {
            List<ProductAllViewModel> memoryVM = productService.GetProducts<Memory>()
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

            ViewBag.Manufacturers = memoryVM.Select(x => x.Manufacturer).Distinct().ToList();
            ViewBag.Models = memoryVM.Select(x => x.Model).Distinct().ToList();

            return View("~/Views/Products/All.cshtml", memoryVM);
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult AllMemories(string filter, int minPrice, int maxPrice, List<string> manufacturers, List<string> models)
        {
            var oldProducts = productService.GetProducts<Memory>();
            List <ProductAllViewModel> memoryVM = productService.Search(filter, minPrice, maxPrice, manufacturers, models, oldProducts)
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

            return View("~/Views/Products/All.cshtml", memoryVM);
        }

        public IActionResult CreateMemory()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateMemory(MemoryCreateViewModel createVm)
        {
            if (ModelState.IsValid)
            {
                var isCreated = service.CreateMemory(createVm.MemoryType, createVm.FormFactor, createVm.Capacity, createVm.ReadSpeed, createVm.WriteSpeed,
                    createVm.Barcode, createVm.Manufacturer, createVm.Model, createVm.Warranty, createVm.Price, createVm.Quantity, createVm.Image);

                if (isCreated)
                {
                    return RedirectToAction("AllMemories");
                }
            }
            return View();
        }
        public IActionResult EditMemory(string id)
        {
            Memory item = productService.GetProduct(id);

            if (item == null)
            {
                return NotFound();
            }
            var editModel = new MemoryCreateViewModel
            {
                Id = item.Id,
                MemoryType = item.MemoryType,
                FormFactor = item.FormFactor,
                Capacity = item.Capacity,
                ReadSpeed = item.ReadSpeed,
                WriteSpeed = item.WriteSpeed,
                Barcode = item.Barcode,
                Manufacturer = item.Manufacturer,
                Model = item.Model,
                Price = item.Price,
                Warranty = item.Warranty,
                Quantity = item.Quantity,
                Image = item.Image
            };

            return View(editModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditMemory(string id, MemoryCreateViewModel createVm)
        {
            if (ModelState.IsValid)
            {
                var isUpdated = service.UpdateMemory(createVm.Id, createVm.MemoryType, createVm.FormFactor, createVm.Capacity, createVm.ReadSpeed, createVm.WriteSpeed,
                    createVm.Barcode, createVm.Manufacturer, createVm.Model, createVm.Warranty, createVm.Price, createVm.Quantity, createVm.Image);

                if (isUpdated)
                {
                    return RedirectToAction("AllMemories");
                }
            }
            return View(createVm);
        }

        //-------ComputerCase-------------
        [AllowAnonymous]
        public IActionResult AllComputerCases()
        {
            List<ProductAllViewModel> computerCaseVM = productService.GetProducts<ComputerCase>()
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

            ViewBag.Manufacturers = computerCaseVM.Select(x => x.Manufacturer).Distinct().ToList();
            ViewBag.Models = computerCaseVM.Select(x => x.Model).Distinct().ToList();

            return View("~/Views/Products/All.cshtml", computerCaseVM);
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult AllComputerCases(string filter, int minPrice, int maxPrice, List<string> manufacturers, List<string> models)
        {
            var oldProducts = productService.GetProducts<ComputerCase>();
            List <ProductAllViewModel> computerCaseVM = productService.Search(filter, minPrice, maxPrice, manufacturers, models, oldProducts)
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

            return View("~/Views/Products/All.cshtml", computerCaseVM);
        }

        public IActionResult CreateComputerCase()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateComputerCase(ComputerCaseCreateViewModel createVm)
        {
            if (ModelState.IsValid)
            {
                var isCreated = service.CreateComputerCase(createVm.CaseType, createVm.FormFactor, createVm.CaseSize, createVm.Color,
                    createVm.Barcode, createVm.Manufacturer, createVm.Model, createVm.Warranty, createVm.Price, createVm.Quantity, createVm.Image);

                if (isCreated)
                {
                    return RedirectToAction("AllComputerCases");
                }
            }
            return View();
        }
        public IActionResult EditComputerCase(string id)
        {
            ComputerCase item = productService.GetProduct(id);

            if (item == null)
            {
                return NotFound();
            }
            var editModel = new ComputerCaseCreateViewModel
            {
                Id = item.Id,
                CaseType = item.CaseType,
                FormFactor = item.FormFactor,
                CaseSize = item.CaseSize,
                Color = item.Color,
                Barcode = item.Barcode,
                Manufacturer = item.Manufacturer,
                Model = item.Model,
                Price = item.Price,
                Warranty = item.Warranty,
                Quantity = item.Quantity,
                Image = item.Image
            };

            return View(editModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditComputerCase(string id, ComputerCaseCreateViewModel createVm)
        {
            if (ModelState.IsValid)
            {
                var isUpdated = service.UpdateComputerCase(createVm.Id, createVm.CaseType, createVm.FormFactor, createVm.CaseSize, createVm.Color,
                    createVm.Barcode, createVm.Manufacturer, createVm.Model, createVm.Warranty, createVm.Price, createVm.Quantity, createVm.Image);

                if (isUpdated)
                {
                    return RedirectToAction("AllComputerCases");
                }
            }
            return View(createVm);
        }
    }
}