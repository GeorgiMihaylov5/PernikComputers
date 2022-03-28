using Microsoft.AspNetCore.Identity;
using PernikComputers.Domain;
using System.ComponentModel.DataAnnotations;

namespace PernikComputers.Models
{
    public class EmployeeListingModel :  ApplicationUser
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [MaxLength(10)]    
        public string Phone { get; set; }
        [Required]
        public string JobTitle { get; set; }
        public string UserId { get; set; }
        public bool IsAdmin { get; set; }
    }
}
