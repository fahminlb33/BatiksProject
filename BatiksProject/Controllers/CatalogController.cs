using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BatiksProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace BatiksProject.Controllers
{
    public class CatalogController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Navbar = NavbarClass.Catalog;
            return View();
        }

        public IActionResult Search()
        {
            ViewBag.Navbar = NavbarClass.Search;
            return View();
        }

        public IActionResult Edit()
        {

            return View();
        }
    }
}
