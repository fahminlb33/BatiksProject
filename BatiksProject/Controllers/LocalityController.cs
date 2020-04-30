using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BatiksProject.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BatiksProject.Controllers
{
    public class LocalityController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Sidebar = SidebarClass.LocalityManage;
            return View("Manage");
        }

        public IActionResult Add()
        {
            ViewBag.Sidebar = SidebarClass.LocalityAddOrEdit;
            return View("Manage");
        }

        public IActionResult Edit(string locality)
        {
            ViewBag.Sidebar = SidebarClass.LocalityAddOrEdit;
            return View("Manage");
        }

        public IActionResult Save()
        {
            return RedirectToActionPermanent("Index");
        }
    }
}
