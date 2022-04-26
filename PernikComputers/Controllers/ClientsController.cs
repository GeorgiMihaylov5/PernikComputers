using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Manage.Internal;
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
    public class ClientsController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IClientService service;
        private readonly IProductService productService;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ILogger<ChangePasswordModel> logger;

        public ClientsController(UserManager<ApplicationUser> userManager,
            IClientService service,
            IProductService productService,
            SignInManager<ApplicationUser> signInManager,
             ILogger<ChangePasswordModel> logger)
        {
            this.userManager = userManager;
            this.service = service;
            this.productService = productService;
            this.signInManager = signInManager;
            this.logger = logger;
        }
        /// <summary>
        /// Home page
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public IActionResult Index()
        {
            //Dictionary<string, int> map = new Dictionary<string, int>();

            //foreach (var order in orderService.All())
            //{
            //    if (!map.ContainsKey(order.ProductId))
            //    {
            //        map.Add(order.ProductId, 1);
            //    }
            //    else
            //    {
            //        map[order.ProductId]++;
            //    }
            //}
            //var ids = map.OrderByDescending(x => x.Value).Take(6).Select(x => x.Key).ToList();
            //var products = productService.GetAllProducts().Where(x => ids.Contains(x.Id)).ToList();
            var products = productService.GetAllProducts().OrderByDescending(x => x.Orders.Count).Take(6).ToList();

            List<ProductAllViewModel> productVm = products
               .Select(x => new ProductAllViewModel
               {
                   Id = x.Id,
                   Manufacturer = x.Manufacturer,
                   Model = x.Model,
                   Price = x.Price,
                   Image = x.Image,
                   Category = x.Category,
                   Discount = x.Discount,
                   Description = new List<string>()
               }).ToList();

            return View(productVm);
        }
        /// <summary>
        /// About us page
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public IActionResult AboutUs()
        {
            return View();
        }
        /// <summary>
        /// Contact page
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public IActionResult Contacts()
        {
            return View();
        }
        /// <summary>
        /// View all clients
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Administrator")]
        public IActionResult All()
        {
            var employees = service.GetClients()
                .Select(x => new ClientListingModel
                {
                    Id = x.Id,
                    FullName = service.GetFullName(x.Id),
                    Phone = x.Phone,
                    Address = x.Address,
                    UserId = x.UserId,
                    UserName = x.User.UserName,
                    Email = x.User.Email
                }).ToList();


            employees = employees.OrderBy(x => x.UserName).ToList();
            return View(employees);
        }
        /// <summary>
        /// Register page for a new user
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public IActionResult Register()
        {
            ViewBag.Users = new List<string>(userManager.Users.Select(x => x.UserName));
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<ActionResult> Register(ClientCreateViewModel client)
        {
            ViewBag.Users = new List<string>(userManager.Users.Select(x => x.UserName));
            if (!ModelState.IsValid)
            {
                return View(client);
            }
            if (await userManager.FindByNameAsync(client.Username) == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = client.Username;
                user.Email = client.Email;

                var result = await userManager.CreateAsync(user, client.Password);

                if (result.Succeeded)
                {
                    var created = service.CreateClient(client.FirstName, client.LastName, client.PhoneNumber, client.Address, user.Id);

                    if (created)
                    {
                        userManager.AddToRoleAsync(user, "Client").Wait();

                        await signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("AllProcessors", "Components");
                    }
                }
            }
            return View();
        }
        /// <summary>
        /// Edit current user information
        /// </summary>
        /// <returns></returns>
        public IActionResult Profile()
        {
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var clientEdit = new ClientCreateViewModel();

            if (User.IsInRole("Employee") || User.IsInRole("Administrator"))
            {
                return RedirectToAction("Profile", "Employees");
            }
            else
            {
                var client = service.GetClient(userId);

                clientEdit.Id = client.Id;
                clientEdit.FirstName = client.FirstName;
                clientEdit.LastName = client.LastName;
                clientEdit.Username = client.User.UserName;
                clientEdit.Email = client.User.Email;
                clientEdit.PhoneNumber = client.Phone;
                clientEdit.Address = client.Address;
            }

            return View(clientEdit);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Profile(ClientCreateViewModel editVm)
        {
            var isUpdated = service.Update(editVm.Id, editVm.FirstName, editVm.LastName, editVm.PhoneNumber, editVm.Address);

            if (isUpdated)
            {
                return RedirectToAction("Profile");
            }

            return RedirectToAction("Index", "Clients");
        }

        /// <summary>
        /// Change password on current user
        /// </summary>
        /// <returns></returns>
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
            }

            var changePasswordResult = await userManager.ChangePasswordAsync(user, viewModel.OldPassword, viewModel.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View();
            }

            await signInManager.RefreshSignInAsync(user);
            logger.LogInformation("User changed their password successfully.");

            return RedirectToAction("Profile");
        }
    }
}
