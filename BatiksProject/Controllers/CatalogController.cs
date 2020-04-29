using System.Collections.Generic;
using BatiksProject.Dto;
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
            var model = new CatalogIndexViewModel
            {
                Items = new List<BatikDto>
                {
                    new BatikDto{BatikId = 1, ImageUrl = "https://source.unsplash.com/WLUHO9A_xik/800x528", Locality = "Bogor", Title = "Batik 1"},
                    new BatikDto{BatikId = 1, ImageUrl = "https://source.unsplash.com/WLUHO9A_xik/800x528", Locality = "Bogor", Title = "Batik 1"},
                    new BatikDto{BatikId = 1, ImageUrl = "https://source.unsplash.com/WLUHO9A_xik/800x528", Locality = "Bogor", Title = "Batik 1"}
                }
            };

            ViewBag.Navbar = NavbarClass.Catalog;
            return View(model);
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

        public IActionResult Search(SearchViewModel model)
        {
            ViewBag.Navbar = NavbarClass.Catalog;
            return View("Index");
        }

        public IActionResult Detail(int batikId)
        {
            return View();
        }
    }
}
