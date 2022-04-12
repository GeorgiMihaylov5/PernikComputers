using Microsoft.AspNetCore.Identity;
using PernikComputers.Domain;
using System.ComponentModel.DataAnnotations;

namespace PernikComputers.Models
{
    public class ClientListingModel : ApplicationUser
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        [Required]
        [MaxLength(10)]
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }
        public string UserId { get; set; }
    }
}
