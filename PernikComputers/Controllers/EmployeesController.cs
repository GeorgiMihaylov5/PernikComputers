using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public EmployeesController(IEmployeeService _service, UserManager<ApplicationUser> _userManager)
        {
            this.userManager = _userManager;
            this.service = _service;
        }
        public async Task<IActionResult> All()
        {
            var employees = service.GetEmployees()
                .Select(x => new EmployeeListingModel
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
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

                        return RedirectToAction("AllProcessors", "Components");
                    }
                }
            }
            ModelState.AddModelError(string.Empty, "The user exists.");
            return View();
        }

        public IActionResult Delete(string id)
        {
            var employee = service.GetEmployee(id);

            if (employee == null)
            {
                return NotFound();
            }

            EmployeeListingModel employeeViewModel = new EmployeeListingModel
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Phone = employee.Phone,
                JobTitle = employee.JobTitle,
                UserId = employee.UserId,
                UserName = employee.User.UserName,
                Email = employee.User.Email
            };

            return View(employeeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(string id, string hi, IFormCollection collection)
        {
            if (hi == "true")
            {
                var isDeleted = service.Remove(id);
                if (isDeleted)
                {
                    return this.RedirectToAction("All");
                }
            }
            return View();
        }
        public IActionResult Profile()
        {
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var clientEdit = new ClientCreateViewModel();

            var employee = service.GetEmployee(userId);

            clientEdit.FirstName = employee.FirstName;
            clientEdit.LastName = employee.LastName;
            clientEdit.Username = employee.User.UserName;
            clientEdit.Email = employee.User.Email;
            clientEdit.PhoneNumber = employee.Phone;

            return View(clientEdit);
        }
    }
}
