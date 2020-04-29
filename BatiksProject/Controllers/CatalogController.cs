using System.Threading.Tasks;
using BatiksProject.Services;
using BatiksProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BatiksProject.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ILogger<CatalogController> _logger;
        private readonly ICatalogService _catalogService;

        public CatalogController(ILogger<CatalogController> logger, ICatalogService catalogService)
        {
            _logger = logger;
            _catalogService = catalogService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return View("Manage");
            }
            else
            {
                var model = new CatalogIndexViewModel
                {
                    Items =  await _catalogService.GetAll()
                };

                ViewBag.Navbar = NavbarClass.Catalog;
                return View(model);
            }
        }

        [Authorize]
        [HttpGet]
        public IActionResult Manage()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public IActionResult Edit()
        {

            return View();
        }

        [HttpGet]
        public IActionResult Search(SearchViewModel model)
        {
            ViewBag.Navbar = NavbarClass.Catalog;
            return View("Index");
        }

        [HttpGet]
        public IActionResult Detail(int batikId)
        {
            return View();
        }
    }
}
