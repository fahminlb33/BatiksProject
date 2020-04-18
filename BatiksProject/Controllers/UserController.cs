using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BatiksProject.Controllers
{
    public class UserController : Controller
    {

        public IActionResult Index()
        {
            return View("Manage");
        }

        public IActionResult Edit()
        {
            return View();
        }
    }
}
