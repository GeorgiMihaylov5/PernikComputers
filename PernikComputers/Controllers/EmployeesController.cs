using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PernikComputers.Abstraction;
using PernikComputers.Domain;
using PernikComputers.Models;
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
        // GET: EmployeeController
        public IActionResult Index()
        {
            return View();
        }

        // GET: EmployeeController/Details/5
        public IActionResult Details(int id)
        {
            return View();
        }

        // GET: EmployeeController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EmployeeController/Create
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

        // GET: EmployeeController/Edit/5
        public IActionResult Edit(int id)
        {
            return View();
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeController/Delete/5
        public IActionResult Delete(int id)
        {
            return View();
        }

        // POST: EmployeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
