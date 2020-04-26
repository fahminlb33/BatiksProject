using BatiksProject.Models;
using BatiksProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        public IActionResult Manage()
        {
            return View();
        }

        [Authorize]
        public IActionResult Edit()
        {

            return View();
        }
    }
}
