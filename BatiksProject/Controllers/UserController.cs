using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BatiksProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BatiksProject.Controllers
{
    public class UserController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            ViewBag.Sidebar = SidebarClass.UserManage;
            return View("Manage");
        }

        [Authorize]
        public IActionResult Profile()
        {
            ViewBag.Sidebar = SidebarClass.UserAddOrEdit;
            return View("Edit");
        }

        [Authorize]
        public IActionResult Add()
        {
            ViewBag.Sidebar = SidebarClass.UserAddOrEdit;
            return View("Edit");
        }

        [Authorize]
        public IActionResult Edit(int userId)
        {
            ViewBag.Sidebar = SidebarClass.UserAddOrEdit;
            return View();
        }

        [Authorize]
        public IActionResult Save()
        {
            return RedirectToActionPermanent("Index");
        }
    }
}
