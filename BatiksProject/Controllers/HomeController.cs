using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BatiksProject.Models;

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
            ViewBag.Navbar = NavbarClass.Home;
            return View();
        }

        public IActionResult About()
        {
            ViewBag.Navbar = NavbarClass.About;
            return View();
        }

    }
}
