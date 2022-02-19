using Microsoft.AspNetCore.Mvc;
using PernikComputers.Abstraction;

namespace PernikComputers.Controllers
{
    public class ComponentsController : Controller
    {
        private readonly IComponentsService service;

        public ComponentsController(IComponentsService _service)
        {
            this.service = _service;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
