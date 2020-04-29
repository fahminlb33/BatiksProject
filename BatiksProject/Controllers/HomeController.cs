using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using BatiksProject.Services;
using BatiksProject.ViewModels;

namespace BatiksProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICatalogService _catalogService;

        public HomeController(ILogger<HomeController> logger, ICatalogService catalogService)
        {
            _logger = logger;
            _catalogService = catalogService;
        }

        public async Task<IActionResult> Index()
        {
            var model = new HomeIndexViewModel
            {
                LatestBatik = await _catalogService.GetLatestBatik()
            };

            ViewBag.Navbar = NavbarClass.Home;
            _logger.LogDebug("Home controller, Index action.");
            return View(model);
        }

        public IActionResult About()
        {
            ViewBag.Navbar = NavbarClass.About;
            _logger.LogDebug("Home controller, About action.");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult StatusCode([FromQuery] string code)
        {
            var model = new StatusCodeViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                ErrorStatusCode = code
            };

            var statusCodeReExecuteFeature = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            if (statusCodeReExecuteFeature != null)
            {
                model.OriginalURL =
                    statusCodeReExecuteFeature.OriginalPathBase
                    + statusCodeReExecuteFeature.OriginalPath
                    + statusCodeReExecuteFeature.OriginalQueryString;
            }

            _logger.LogError("Status code error: " + code);
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            var model = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                Referrer = HttpContext.Request.Headers["referer"],
                ExceptionMessage = exceptionHandlerPathFeature?.Error is FileNotFoundException ? "File error thrown" : ""
            };

            _logger.LogError(exceptionHandlerPathFeature?.Error, "Internal server error");
            return View(model);
        }

    }
}
