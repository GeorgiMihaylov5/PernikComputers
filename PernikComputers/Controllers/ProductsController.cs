using Microsoft.AspNetCore.Mvc;
using PernikComputers.Abstraction;
using PernikComputers.Domain;
using PernikComputers.Domain.Enum;
using PernikComputers.Models;
using System.Collections.Generic;
using System.Linq;

namespace PernikComputers.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService service;

        public ProductsController(IProductService productService)
        {
            this.service = productService;
        }
        public IActionResult All()
        {
            var products = new List<ProductAllViewModel>();
            //List<ProductAllViewModel> productVm = service.GetProducts()
            //   .Select(x => new ProductAllViewModel
            //   {
            //       Id = x.Id,
            //       Manufacturer = x.Manufacturer,
            //       Model = x.Model,
            //       Price = x.Price,
            //       Discount = x.Discount,
            //       Image = x.Image,
            //       Category = x.Category,
            //       Description =  new List<string>()
            //   }).ToList();

            foreach (var product in service.GetProducts())
            {
                var viewModel = new ProductAllViewModel
                {
                    Id = product.Id,
                    Manufacturer = product.Manufacturer,
                    Model = product.Model,
                    Price = product.Price,
                    Discount = product.Discount,
                    Image = product.Image,
                    Category = product.Category
                };

                if (product.Category == Category.Processor)
                {
                    var description = new List<string>()
                    {
                        $"Socket: {((Processor)product).Socket}",
                        $"Operating frequency: {((Processor)product).CPUSpeed} GHz",
                        $"Turbo Boost: {((Processor)product).CPUBoostSpeed} GHz",
                        $"Cores: {((Processor)product).Cores}",
                        $"Threads: {((Processor)product).Threads}",
                    };

                    viewModel.Description = description;
                }

                products.Add(viewModel);
            }

           
            return View(products);
        }
        public IActionResult AllTable()
        {
            List<ProductAllTableViewModel> productVm = service.GetProducts()
               .Select(x => new ProductAllTableViewModel
               {
                   Id = x.Id,
                   Barcode = x.Barcode,
                   Manufacturer = x.Manufacturer,
                   Model = x.Model,
                   Price = x.Price,
                   Image = x.Image,
                   Category = x.Category,
                   Warranty = x.Warranty,
                   Quantity = x.Quantity,
                   Discount = x.Discount
               }).ToList();

            return View(productVm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(string id)
        {
            try
            {
                var isDeleted = service.RemoveProduct(id);
                if (isDeleted)
                {
                    return this.RedirectToAction("AllTable");
                }
                return this.RedirectToAction("All", "Employees");
            }
            catch
            {
                return this.RedirectToAction("All", "Employees");
            }
            
        }
        public IActionResult MakeDiscount(string id)
        {
            var x = service.GetProduct(id);

            ProductPromotionViewModel product = new ProductPromotionViewModel
            {
                Id = x.Id,
                Manufacturer = x.Manufacturer,
                Model = x.Model,
                OldPrice = x.Price,
                NewPrice = x.Price,
                Image = x.Image
            };
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MakeDiscount(string id, int discount)
        {
           service.MakeDiscount(id, discount);

            return this.RedirectToAction("AllTable");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveDiscount(string id, int discount)
        {
            service.RemoveDiscount(id);

            return this.RedirectToAction("AllTable");
        }
        public IActionResult AllDiscounts()
        {
            var products = new List<ProductAllViewModel>();

            foreach (var product in service.GetProducts().Where(x => x.Discount != 0))
            {
                var viewModel = new ProductAllViewModel
                {
                    Id = product.Id,
                    Manufacturer = product.Manufacturer,
                    Model = product.Model,
                    Price = product.Price,
                    Discount = product.Discount,
                    Image = product.Image,
                    Category = product.Category
                };

                if (product.Category == Category.Processor)
                {
                    var description = new List<string>()
                    {
                        $"Socket: {((Processor)product).Socket}",
                        $"Operating frequency: {((Processor)product).CPUSpeed} GHz",
                        $"Turbo Boost: {((Processor)product).CPUBoostSpeed} GHz",
                        $"Cores: {((Processor)product).Cores}",
                        $"Threads: {((Processor)product).Threads}",
                    };

                    viewModel.Description = description;
                }
                else if (product.Category == Category.Motherboard)
                {
                    var description = new List<string>()
                    {
                        $"Socket: {((Motherboard)product).Socket}",
                        $"Chipset: {((Motherboard)product).Chipset}",
                        $"Supported memory: {((Motherboard)product).TypeRam}",
                    };

                    viewModel.Description = description;
                }
                else if (product.Category == Category.Ram)
                {
                    var description = new List<string>()
                    {
                        $"Capacity: {((Ram)product).Size} GB",
                        $"Type: {((Ram)product).TypeRam}",
                        $"Frequency: {((Ram)product).Frequency} MHz",
                    };

                    viewModel.Description = description;
                }
                else if (product.Category == Category.VideoCard)
                {
                    var description = new List<string>()
                    {
                        $"Graphic Processor: {((VideoCard)product).GraphicProcessor}",
                        $"Memory capacity: {((VideoCard)product).SizeMemory } GB",
                        $"Memory type: {((VideoCard)product).TypeMemory}",
                    };

                    viewModel.Description = description;
                }
                else if (product.Category == Category.PowerSupply)
                {
                    var description = new List<string>()
                    {
                        $"Power: {((PowerSupply)product).Power} W",
                        $"Efficiency: {((PowerSupply)product).Efficiency}%"
                    };

                    viewModel.Description = description;
                }
                else if (product.Category == Category.Memory)
                {
                    var description = new List<string>()
                    {
                        $"Type: {((Memory)product).MemoryType} W",
                        $"Capacity: {((Memory)product).Capacity} GB",
                    };

                    viewModel.Description = description;
                }
                else if (product.Category == Category.ComputerCase)
                {
                    var description = new List<string>()
                    {
                        $"Type: {((ComputerCase)product).CaseType} W",
                        $"Form factor: {((ComputerCase)product).FormFactor}",
                        $"Size: {((ComputerCase)product).CaseSize} mm",
                    };

                    viewModel.Description = description;
                }
                else if (product.Category == Category.Computer)
                {
                    var description = new List<string>()
                    {
                        $"{((Computer)product).Processor.Manufacturer} {((Computer)product).Processor.Model} ({((Computer)product).Processor.CPUSpeed}/{((Computer)product).Processor.CPUBoostSpeed} GHz, {((Computer)product).Processor.Cache} M)",
                        $"{((Computer)product).VideoCard.Manufacturer} {((Computer)product).VideoCard.Model} {((Computer)product).VideoCard.SizeMemory} GB",
                        $"{((Computer)product).Ram.Size} GB {((Computer)product).Ram.TypeRam} {((Computer)product).Ram.Frequency} MHz"
                    };

                    viewModel.Description = description;
                }

                products.Add(viewModel);
            }
            return View("All", products);
        }
    }
}
