﻿using Microsoft.EntityFrameworkCore;
using PernikComputers.Abstraction;
using PernikComputers.Data;
using PernikComputers.Domain;
using PernikComputers.Domain.Enum;
using System.Collections.Generic;
using System.Linq;

namespace PernikComputers.Service
{
    public class ComputerService : IComputerService
    {
        private readonly ApplicationDbContext context;

        public ComputerService(ApplicationDbContext _context)
        {
            this.context = _context;
        }

        public bool Create(string processorId, string motherboardId, string ramId, string videoCardId, 
            string powerSupplyId, string memoryId, string computerCaseId, 
            string barcode, string manufacturer, string model, int warranty, int quantity)
        {
            var computer = new Computer
            {
                ProcessorId = processorId,
                MotherboardId = motherboardId,
                RamId = ramId,
                VideoCardId = videoCardId,
                PowerSupplyId = powerSupplyId,
                MemoryId = memoryId,
                ComputerCaseId = computerCaseId,
                Barcode = barcode,
                Manufacturer = manufacturer,
                Model = model,
                Warranty = warranty,
                Quantity = quantity,
                Category = Category.Computer,
                Image = context.ComputerCases.Find(computerCaseId).Image,
                Processor =  context.Processors.Find(processorId),
                Motherboard = context.Motherboards.Find(motherboardId),
                Ram = context.Rams.Find(ramId),
                VideoCard =   context.VideoCards.Find(videoCardId),
                PowerSupply = context.PowerSupplies.Find(powerSupplyId),
                Memory = context.Memories.Find(memoryId),
                ComputerCase = context.ComputerCases.Find(computerCaseId)
            };

            computer.Price = computer.Processor.Price + computer.Motherboard.Price +
                computer.Ram.Price + computer.VideoCard.Price +
                computer.PowerSupply.Price + computer.Memory.Price +
                computer.ComputerCase.Price;
            context.Computers.Add(computer);

            Product[] components =
            {
                context.Processors.Find(processorId),
                context.Motherboards.Find(motherboardId),
                context.Rams.Find(ramId),
                context.VideoCards.Find(videoCardId),
                context.PowerSupplies.Find(powerSupplyId),
                context.Memories.Find(memoryId),
                context.ComputerCases.Find(computerCaseId),
            };

            foreach (var item in components)
            {
                item.Quantity -= quantity;
                context.Update(item);
            }
            //var processor = context.Processors.Find(processorId);
            //var motherboard = context.Motherboards.Find(motherboardId);
            //var ram = context.Rams.Find(ramId);
            //var videoCard = context.VideoCards.Find(videoCardId);
            //var powerSupply = context.PowerSupplies.Find(powerSupplyId);
            //var memory = context.Memories.Find(memoryId);
            //var computerCase = context.ComputerCases.Find(computerCaseId);

            return context.SaveChanges() != 0;
        }

        public bool UpdateComputer(string id, string processorId, string motherboardId, string ramId,
            string videoCardId, string powerSupplyId, string memoryId, string computerCaseId,
            string barcode, string manufacturer, string model, int warranty, decimal price, int quantity, string image)
        {
            var computer = context.Computers.Find(id);

            if (computer == null)
            {
                return false;
            }

            var newQuantity = quantity - computer.Quantity;
            computer.ProcessorId = processorId;
            computer.MotherboardId = motherboardId;
            computer.RamId = ramId;
            computer.VideoCardId = videoCardId;
            computer.PowerSupplyId = powerSupplyId;
            computer.MemoryId = memoryId;
            computer.ComputerCaseId = computerCaseId;
            computer.Barcode = barcode;
            computer.Manufacturer = manufacturer;
            computer.Model = model;
            computer.Price = price;
            computer.Warranty = warranty;
            computer.Quantity = quantity;
            computer.Image = image;

            Product[] components =
            {
                context.Processors.Find(processorId),
                context.Motherboards.Find(motherboardId),
                context.Rams.Find(ramId),
                context.VideoCards.Find(videoCardId),
                context.PowerSupplies.Find(powerSupplyId),
                context.Memories.Find(memoryId),
                context.ComputerCases.Find(computerCaseId),
            };

            foreach (var item in components)
            {
                item.Quantity -= newQuantity;
                context.Update(item);
            }


            context.Computers.Update(computer);
            return context.SaveChanges() != 0;
        }
    }
}
