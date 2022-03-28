using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PernikComputers.Abstraction;
using PernikComputers.Domain;
using PernikComputers.Models;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PernikComputers.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService service;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ILogger<LogoutModel> logger;

        public EmployeesController(IEmployeeService service,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<LogoutModel> logger)
        {
            this.service = service;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
        }

        public async Task<IActionResult> All()
        {
            var employees = service.GetEmployees()
                .Select(x => new EmployeeListingModel
                {
                    Id = x.Id,
                    FullName = service.GetFullName(x.Id),
                    Phone = x.Phone,
                    JobTitle = x.JobTitle,
                    UserId = x.UserId,
                    UserName = x.User.UserName,
                    Email = x.User.Email
                }).ToList();

            var adminIds = (await userManager.GetUsersInRoleAsync("Administrator"))
                .Select(x => x.Id).ToList();

            foreach (var employee in employees)
            {
                employee.IsAdmin = adminIds.Contains(employee.UserId);
            }

            employees = employees.OrderByDescending(x => x.IsAdmin).ThenBy(x => x.UserName).ToList();
            return View(employees);
        }

        [HttpPost]
        public async Task<IActionResult> Promote(string userId)
        {
            if (userId == null)
            {
                return RedirectToAction("All");
            }
            var user = await userManager.FindByIdAsync(userId);

            if (user == null || await userManager.IsInRoleAsync(user, "Administrator"))
            {
                return RedirectToAction("All");
            }
            await userManager.AddToRoleAsync(user, "Administrator");

            return RedirectToAction("All");
        }
        [HttpPost]
        public async Task<IActionResult> Demote(string userId)
        {
            if (userId == null)
            {
                return RedirectToAction("All");
            }
            var user = await userManager.FindByIdAsync(userId);

            if (user == null || !await userManager.IsInRoleAsync(user, "Administrator"))
            {
                return RedirectToAction("All");
            }
            await userManager.RemoveFromRoleAsync(user, "Administrator");

            return RedirectToAction("All");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeCreateViewModel employee)
        {
            if (!ModelState.IsValid)
            {
                return View(employee);
            }
            if (await userManager.FindByNameAsync(employee.Username) == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = employee.Username;
                user.Email = employee.Email;

                var result = await userManager.CreateAsync(user, "employee123");

                if (result.Succeeded)
                {
                    var created = service.CreateEmployee(employee.FirstName, employee.LastName, employee.PhoneNumber, employee.JobTitle, user.Id);

                    if (created)
                    {
                        userManager.AddToRoleAsync(user, "Employee").Wait();

                        return RedirectToAction("All");
                    }
                }
            }
            ModelState.AddModelError(string.Empty, "The user exists.");
            return View();
        }

        //public IActionResult Delete(string id)
        //{
        //    var employee = service.GetEmployee(id);

        //    if (employee == null)
        //    {
        //        return NotFound();
        //    }

        //    EmployeeListingModel employeeViewModel = new EmployeeListingModel
        //    {
        //        Id = employee.Id,
        //        FirstName = employee.FirstName,
        //        LastName = employee.LastName,
        //        Phone = employee.Phone,
        //        JobTitle = employee.JobTitle,
        //        UserId = employee.UserId,
        //        UserName = employee.User.UserName,
        //        Email = employee.User.Email
        //    };

        //    return View(employeeViewModel);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(string id)
        {
            var isDeleted = service.Remove(id);
            if (isDeleted)
            {
                return RedirectToAction("All", "Employees");
            }

            return RedirectToAction("Profile");
        }
        public IActionResult Profile()
        {
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var employeeEdit = new ClientCreateViewModel();
            var admin = userManager.Users
                   .FirstOrDefault(x => x.Id == userId);

            var employee = service.GetEmployee(userId);

            if (admin.UserName == "admin")
            {            
                employeeEdit.Id = admin.Id;
                employeeEdit.FirstName = "Admin";
                employeeEdit.LastName = "Admin";
                employeeEdit.Username = admin.UserName;
                employeeEdit.Email = admin.Email;
                employeeEdit.PhoneNumber = "*********";
            }
            else
            {
                employeeEdit.Id = employee.Id;
                employeeEdit.FirstName = employee.FirstName;
                employeeEdit.LastName = employee.LastName;
                employeeEdit.Username = employee.User.UserName;
                employeeEdit.Email = employee.User.Email;
                employeeEdit.PhoneNumber = employee.Phone;
            }

            return View("~/Views/Clients/Profile.cshtml", employeeEdit);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Profile(EmployeeCreateViewModel editVm)
        {
            var isUpdated = service.Update(editVm.Id, editVm.FirstName, editVm.LastName, editVm.PhoneNumber);

            if (isUpdated)
            {
                return RedirectToAction("Profile");
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
