﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PernikComputers.Abstraction;
using PernikComputers.Domain;
using PernikComputers.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PernikComputers.Controllers
{
    [Authorize]
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
        /// <summary>
        /// See all employees
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Administrator")]
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
        /// <summary>
        /// Promote to administrator
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Administrator")]
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
        /// <summary>
        /// Demote to employee
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Administrator")]
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
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            ViewBag.Users = new List<string>(userManager.Users.Select(x => x.UserName));
            return View();
        }

        /// <summary>
        /// Create employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create(EmployeeCreateViewModel employee)
        {
            ViewBag.Users = new List<string>(userManager.Users.Select(x => x.UserName));
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
        /// <summary>
        /// Edit current employee information
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Employee,Administrator")]
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

            return RedirectToAction("Index", "Clients");
        }
    }
}
