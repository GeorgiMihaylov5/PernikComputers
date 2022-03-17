using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PernikComputers.Abstraction;
using PernikComputers.Domain;
using PernikComputers.Models;
using System.Threading.Tasks;

namespace PernikComputers.Controllers
{
    public class ClientsController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IClientService service;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ClientsController(UserManager<ApplicationUser> userManager, IClientService service, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.service = service;
            this._signInManager = signInManager;
        }
        // GET: ClientsController
        public IActionResult Index()
        {
            return View();
        }

        // GET: ClientsController/Details/5
        public IActionResult Details(int id)
        {
            return View();
        }

        // GET: ClientsController/Create
        public IActionResult Register()
        {
            return View();
        }

        // POST: ClientsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(ClientCreateViewModel client)
        {
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

                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("AllProcessors", "Components");
                    }
                }
            }
            ModelState.AddModelError(string.Empty, "The client exists.");
            return View();
        }

        // GET: ClientsController/Edit/5
        public IActionResult Edit(int id)
        {
            return View();
        }

        // POST: ClientsController/Edit/5
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

        // GET: ClientsController/Delete/5
        public IActionResult Delete(int id)
        {
            return View();
        }

        // POST: ClientsController/Delete/5
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
