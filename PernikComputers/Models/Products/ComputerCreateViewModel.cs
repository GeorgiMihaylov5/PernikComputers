using PernikComputers.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PernikComputers.Models
{
    public class ComputerCreateViewModel
    {
        public string Id { get; set; }
        [Required]
        public string Barcode { get; set; }
        [Required]
        public string Manufacturer { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public int Warranty { get; set; }
        public decimal Price { get; set; }
        [Required]
        public string ProcessorId { get; set; }
        public Processor Processor { get; set; }
        [Required]
        public string MotherboardId { get; set; }
        public Motherboard Motherboard { get; set; }
        [Required]
        public string RamId { get; set; }
        public Ram Ram { get; set; }
        [Required]
        public string VideoCardId { get; set; }
        public VideoCard VideoCard { get; set; }
        [Required]
        public string PowerSupplyId { get; set; }
        public PowerSupply PowerSupply { get; set; }
        [Required]
        public string MemoryId { get; set; }
        public Memory Memory { get; set; }
        [Required]
        public string ComputerCaseId { get; set; }
        public ComputerCase ComputerCase { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be a positive number.")]
        public int Quantity { get; set; }
        public string Image { get; set; }
    }
}
