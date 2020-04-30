using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BatiksProject.Infrastructure;
using BatiksProject.Services;
using BatiksProject.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BatiksProject.Controllers
{
    public class AdminController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAdminService _adminService;
        private readonly IUserService _userService;

        public AdminController(IMapper mapper, IUserService userService, IAdminService adminService)
        {
            _mapper = mapper;
            _userService = userService;
            _adminService = adminService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var summary = await _adminService.GetSummary();
            var model = _mapper.Map<DashboardViewModel>(summary);

            ViewBag.Sidebar = SidebarClass.Dashboard;
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToActionPermanent("Index");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
            {
                var result = await _userService.Verify(model.Username, model.Password);
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, result.UserId.ToString()),
                    new Claim(ClaimTypes.Name,result.Username),
                    new Claim(ClaimTypes.Role, "User"),
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));

                return RedirectToActionPermanent("Index");
            }
            catch (ServicesException e)
            {
                ViewBag.Message = e.Message;
                return View("Login");
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToActionPermanent("Index", "Home");
        }
    }
}
 