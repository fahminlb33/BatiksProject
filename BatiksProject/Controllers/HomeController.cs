using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BatiksProject.Models;
using Microsoft.AspNetCore.Diagnostics;
using System.IO;
using BatiksProject.Dto;
using BatiksProject.ViewModels;

namespace BatiksProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var model = new HomeIndexViewModel
            {
                LatestBatik = new List<BatikDto>
                {
                    new BatikDto{BatikId = 1, ImageUrl = "https://source.unsplash.com/WLUHO9A_xik/800x528", Locality = "Bogor", Title = "Batik 1"},
                    new BatikDto{BatikId = 1, ImageUrl = "https://source.unsplash.com/WLUHO9A_xik/800x528", Locality = "Bogor", Title = "Batik 1"},
                    new BatikDto{BatikId = 1, ImageUrl = "https://source.unsplash.com/WLUHO9A_xik/800x528", Locality = "Bogor", Title = "Batik 1"}
                }
            };

            ViewBag.Navbar = NavbarClass.Home;
            return View(model);
        }

        public IActionResult About()
        {
            ViewBag.Navbar = NavbarClass.About;
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

            return View(model);
        }

    }
}
