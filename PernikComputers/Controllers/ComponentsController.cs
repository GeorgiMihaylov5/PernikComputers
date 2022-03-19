using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PernikComputers.Abstraction;
using PernikComputers.Domain.Enum;
using PernikComputers.Models;
using System.Collections.Generic;
using System.Linq;

namespace PernikComputers.Controllers
{
    public class ComponentsController : Controller
    {
        private readonly IComponentService service;

        public ComponentsController(IComponentService _service)
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
                    Image = x.Image,
                    DetailsAction = "DetailsProcessor"
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
        public IActionResult DetailsProcessor(string id)
        {
            var x = service.GetProcessor(id);
            ProductDetailsViewModel detailsViewModel = new ProductDetailsViewModel
            {
                Id = x.Id,
                Barcode = x.Barcode,
                Model = x.Model,
                Manufacturer = x.Manufacturer,
                IsPromotion = x.IsPromotion,
                Description = new List<string>(),
                Category = Category.Processor,
                Price = x.Price,
                Quantity = x.Quantity,
                Image = x.Image,
            };

            detailsViewModel.Description.Add($"Socket: {x.Socket}");
            detailsViewModel.Description.Add($"Operating frequency: {x.CPUSpeed} GHz");
            detailsViewModel.Description.Add($"Turbo Boost: {x.CPUBoostSpeed} GHz");
            detailsViewModel.Description.Add($"Cores: {x.Cores}");
            detailsViewModel.Description.Add($"Threads: {x.Threads}");
            detailsViewModel.Description.Add($"Cashe: {x.Cache} MB");
            detailsViewModel.Description.Add($"Warranty: {x.Warranty} months");

            return View("Details", detailsViewModel);
        }

        public IActionResult EditProcessor(string id)
        {
            var item = service.GetProcessor(id);

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

            return View(editModel);
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
                    return RedirectToAction("AllProcessors");
                }
            }
            return View(createVm);
        }

        public IActionResult DeleteProcessor(string id)
        {
            var item = service.GetProcessor(id);

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

            return View(editModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteProcessor(string id, IFormCollection collection)
        {
            var isDeleted = service.RemoveProcessor(id);
            if (isDeleted)
            {
                return this.RedirectToAction("AllProcessors");
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
                    Image = x.Image,
                    DetailsAction = "DetailsMotherboard"
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
        public IActionResult DetailsMotherboard(string id)
        {
            var x = service.GetMotherboard(id);
            ProductDetailsViewModel detailsViewModel = new ProductDetailsViewModel
            {
                Id = x.Id,
                Barcode = x.Barcode,
                Model = x.Model,
                Manufacturer = x.Manufacturer,
                IsPromotion = x.IsPromotion,
                Category = Category.Processor,
                Description = new List<string>(),
                Price = x.Price,
                Quantity = x.Quantity,
                Image = x.Image,
            };

            detailsViewModel.Description.Add($"Socket: {x.Socket}");
            detailsViewModel.Description.Add($"Chipset: {x.Chipset}");
            detailsViewModel.Description.Add($"Supported memory: {x.TypeRam}");
            detailsViewModel.Description.Add($"Number of memory slots: {x.RamSlotsCount}");
            detailsViewModel.Description.Add($"Form factor: {x.FormFactor}");
            detailsViewModel.Description.Add($"Warranty: {x.Warranty} months");

            return View("Details", detailsViewModel);
        }

        public IActionResult EditMotherboard(string id)
        {
            var item = service.GetMotherboard(id);

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

        public IActionResult DeleteMotherboard(string id)
        {
            var item = service.GetMotherboard(id);

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
        public IActionResult DeleteMotherboard(string id, IFormCollection collection)
        {
            var isDeleted = service.RemoveMotherboard(id);
            if (isDeleted)
            {
                return this.RedirectToAction("AllMotherboards");
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
                    Image = x.Image,
                    DetailsAction = "DetailsRam"
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
        public IActionResult DetailsRam(string id)
        {
            var x = service.GetRam(id);
            ProductDetailsViewModel detailsViewModel = new ProductDetailsViewModel
            {
                Id = x.Id,
                Barcode = x.Barcode,
                Model = x.Model,
                Manufacturer = x.Manufacturer,
                IsPromotion = x.IsPromotion,
                Category = Category.Processor,
                Description = new List<string>(),
                Price = x.Price,
                Quantity = x.Quantity,
                Image = x.Image,
            };

            detailsViewModel.Description.Add($"Capacity: {x.Size} GB");
            detailsViewModel.Description.Add($"Type: {x.TypeRam}");
            detailsViewModel.Description.Add($"Frequency: {x.Frequency} MHz");
            detailsViewModel.Description.Add($"Timing: {x.Timing}");
            detailsViewModel.Description.Add($"Warranty: {x.Warranty} months");

            return View("Details", detailsViewModel);
        }
        public IActionResult EditRam(string id)
        {
            var item = service.GetRam(id);

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

        public IActionResult DeleteRam(string id)
        {
            var item = service.GetRam(id);

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
        public IActionResult DeleteRam(string id, IFormCollection collection)
        {
            var isDeleted = service.RemoveRam(id);
            if (isDeleted)
            {
                return this.RedirectToAction("AllRams");
            }
            return View();
        }

        //-------VideoCard-------------

        public IActionResult AllVideoCards()
        {
            List<ProductAllViewModel> videoCardVM = service.GetVideoCards()
                .Select(x => new ProductAllViewModel
                {
                    Id = x.Id,
                    Manufacturer = x.Manufacturer,
                    Model = x.Model,
                    Price = x.Price,
                    IsPromotion = x.IsPromotion,
                    Image = x.Image,
                    DetailsAction = "DetailsVideoCard"
                }).ToList();


            return View("All", videoCardVM);
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

        public IActionResult DetailsVideoCard(string id)
        {
            var x = service.GetVideoCard(id);
            ProductDetailsViewModel detailsViewModel = new ProductDetailsViewModel
            {
                Id = x.Id,
                Barcode = x.Barcode,
                Model = x.Model,
                Manufacturer = x.Manufacturer,
                IsPromotion = x.IsPromotion,
                Description = new List<string>(),
                Category = Category.Processor,
                Price = x.Price,
                Quantity = x.Quantity,
                Image = x.Image,
            };

            detailsViewModel.Description.Add($"Chip manufacturer: {x.ChipManufacturer }");
            detailsViewModel.Description.Add($"Graphic Processor: {x.GraphicProcessor}");
            detailsViewModel.Description.Add($"Memory capacity: {x.SizeMemory } GB");
            detailsViewModel.Description.Add($"Memory type: {x.TypeMemory}");
            detailsViewModel.Description.Add($"Memory Frequency: {x.MemoryFrequency} MHz");
            detailsViewModel.Description.Add($"Core Frequency: {x.CoreFrequency} MHz");
            detailsViewModel.Description.Add($"Current Processes: {x.CurrentProcesses}");
            detailsViewModel.Description.Add($"Rail Width: {x.RailWidth} bit");
            detailsViewModel.Description.Add($"Slot: {x.SlotType}");
            detailsViewModel.Description.Add($"Warranty: {x.Warranty} months");

            return View("Details", detailsViewModel);
        }

        public IActionResult EditVideoCard(string id)
        {
            var item = service.GetVideoCard(id);

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

        public IActionResult DeleteVideoCard(string id)
        {
            var item = service.GetVideoCard(id);
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
        public IActionResult DeleteVideoCard(string id, IFormCollection collection)
        {
            var isDeleted = service.RemoveVideoCard(id);
            if (isDeleted)
            {
                return this.RedirectToAction("AllVideoCards");
            }
            return View();
        }

        //-------PowerSupply-------------

        public IActionResult AllPowerSupplies()
        {
            List<ProductAllViewModel>powerSupplyVM = service.GetPowerSupplies()
                .Select(x => new ProductAllViewModel
                {
                    Id = x.Id,
                    Manufacturer = x.Manufacturer,
                    Model = x.Model,
                    Price = x.Price,
                    IsPromotion = x.IsPromotion,
                    Image = x.Image,
                    DetailsAction = "DetailsPowerSupply"
                }).ToList();


            return View("All", powerSupplyVM);
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

        public IActionResult DetailsPowerSupply(string id)
        {
            var x = service.GetPowerSupply(id);
            ProductDetailsViewModel detailsViewModel = new ProductDetailsViewModel
            {
                Id = x.Id,
                Barcode = x.Barcode,
                Model = x.Model,
                Manufacturer = x.Manufacturer,
                IsPromotion = x.IsPromotion,
                Category = Category.Processor,
                Description = new List<string>(),
                Price = x.Price,
                Quantity = x.Quantity,
                Image = x.Image,
            };

            detailsViewModel.Description.Add($"Power: {x.Power} W");
            detailsViewModel.Description.Add($"Form factor: {x.FormFactor}");
            detailsViewModel.Description.Add($"Efficiency: {x.Efficiency}%");
            detailsViewModel.Description.Add($"Warranty: {x.Warranty} months");

            return View("Details", detailsViewModel);
        }

        public IActionResult EditPowerSupply(string id)
        {
            var item = service.GetPowerSupply(id);

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

        public IActionResult DeletePowerSupply(string id)
        {
            var item = service.GetPowerSupply(id);

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
        public IActionResult DeletePowerSupply(string id, IFormCollection collection)
        {
            var isDeleted = service.RemovePowerSupply(id);
            if (isDeleted)
            {
                return this.RedirectToAction("AllPowerSupplies");
            }
            return View();
        }

        //-------Memory-------------

        public IActionResult AllMemories()
        {
            List<ProductAllViewModel> memoryVM = service.GetMemories()
                .Select(x => new ProductAllViewModel
                {
                    Id = x.Id,
                    Manufacturer = x.Manufacturer,
                    Model = x.Model,
                    Price = x.Price,
                    IsPromotion = x.IsPromotion,
                    Image = x.Image,
                    DetailsAction = "DetailsMemory"
                }).ToList();


            return View("All", memoryVM);
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
        public IActionResult DetailsMemory(string id)
        {
            var x = service.GetMemory(id);
            ProductDetailsViewModel detailsViewModel = new ProductDetailsViewModel
            {
                Id = x.Id,
                Barcode = x.Barcode,
                Model = x.Model,
                Manufacturer = x.Manufacturer,
                IsPromotion = x.IsPromotion,
                Category = Category.Processor,
                Price = x.Price,
                Description = new List<string>(),
                Quantity = x.Quantity,
                Image = x.Image,
            };

            detailsViewModel.Description.Add($"Type: {x.MemoryType} W");
            detailsViewModel.Description.Add($"Form factor: {x.FormFactor}");
            detailsViewModel.Description.Add($"Capacity: {x.Capacity} GB");
            detailsViewModel.Description.Add($"Reading speed: {x.ReadSpeed} MB/s");
            detailsViewModel.Description.Add($"Recording speed: {x.WriteSpeed} MB/s");
            detailsViewModel.Description.Add($"Warranty: {x.Warranty} months");

            return View("Details", detailsViewModel);
        }

        public IActionResult EditMemory(string id)
        {
            var item = service.GetMemory(id);

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

        public IActionResult DeleteMemory(string id)
        {
            var item = service.GetMemory(id);

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
        public IActionResult DeleteMemory(string id, IFormCollection collection)
        {
            var isDeleted = service.RemoveMemory(id);
            if (isDeleted)
            {
                return this.RedirectToAction("AllMemories");
            }
            return View();
        }

        //-------ComputerCase-------------

        public IActionResult AllComputerCases()
        {
            List<ProductAllViewModel> computerCaseVM = service.GetComputerCases()
                .Select(x => new ProductAllViewModel
                {
                    Id = x.Id,
                    Manufacturer = x.Manufacturer,
                    Model = x.Model,
                    Price = x.Price,
                    IsPromotion = x.IsPromotion,
                    Image = x.Image,
                    DetailsAction = "DetailsComputerCases"
                }).ToList();


            return View("All", computerCaseVM);
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
        public IActionResult DetailsComputerCase(string id)
        {
            var x = service.GetComputerCase(id);
            ProductDetailsViewModel detailsViewModel = new ProductDetailsViewModel
            {
                Id = x.Id,
                Barcode = x.Barcode,
                Model = x.Model,
                Manufacturer = x.Manufacturer,
                IsPromotion = x.IsPromotion,
                Category = Category.Processor,
                Description = new List<string>(),
                Price = x.Price,
                Quantity = x.Quantity,
                Image = x.Image,
            };

            detailsViewModel.Description.Add($"Type: {x.CaseType} W");
            detailsViewModel.Description.Add($"Form factor: {x.FormFactor}");
            detailsViewModel.Description.Add($"Size: {x.CaseSize} mm");
            detailsViewModel.Description.Add($"Reading speed: {x.Color }");
            detailsViewModel.Description.Add($"Warranty: {x.Warranty} months");

            return View("Details", detailsViewModel);
        }

        public IActionResult EditComputerCase(string id)
        {
            var item = service.GetComputerCase(id);

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

        public IActionResult DeleteComputerCase(string id)
        {
            var item = service.GetComputerCase(id);

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
        public IActionResult DeleteComputerCase(string id, IFormCollection collection)
        {
            var isDeleted = service.RemoveComputerCase(id);
            if (isDeleted)
            {
                return this.RedirectToAction("AllComputerCases");
            }
            return View();
        }
    }
}
