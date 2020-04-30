using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BatiksProject.Infrastructure;
using BatiksProject.Models;
using BatiksProject.Services;
using BatiksProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BatiksProject.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(ILogger<UserController> logger, IUserService userService, IMapper mapper)
        {
            _logger = logger;
            _userService = userService;
            _mapper = mapper;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var model = new UserManageViewModel
            {
                Users = await _userService.GetAll()
            };
            
            ViewBag.Sidebar = SidebarClass.UserManage;
            return View("Manage", model);
        }

        [Authorize]
        public IActionResult Profile()
        {
            var model = new UserEditViewModel
            {
                UserId = Convert.ToInt32(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)),
                Username = HttpContext.User.FindFirstValue(ClaimTypes.Name),
                IsEdit = true
            };

            ViewBag.Sidebar = SidebarClass.UserAddOrEdit;
            return View("Edit", model);
        }

        [Authorize]
        public IActionResult Add()
        {
            ViewBag.Sidebar = SidebarClass.UserAddOrEdit;
            return View("Edit", new UserEditViewModel {IsEdit = false});
        }

        [Authorize]
        public async Task<IActionResult> Edit(int userId)
        {
            try
            {
                var user = await _userService.Get(userId);
                var model = _mapper.Map<UserEditViewModel>(user);
                model.IsEdit = true;

                ViewBag.Sidebar = SidebarClass.UserAddOrEdit;
                return View("Edit", model);
            }
            catch (ServicesException serviceException)
            {
                ViewBag.Message = serviceException.Message;
                ViewBag.AlertClass = "warning";
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error when deleting entry");

                ViewBag.Message = "Server mengalami masalah. Coba lagi nanti.";
                ViewBag.AlertClass = "danger";
            }

            return await Index();
        }

        [Authorize]
        public async Task<IActionResult> Delete(int userId)
        {
            try
            {
                await _userService.Remove(userId);

                ViewBag.Message = "Akun telah dihapus.";
                ViewBag.AlertClass = "success";
            }
            catch (ServicesException serviceException)
            {
                ViewBag.Message = serviceException.Message;
                ViewBag.AlertClass = "warning";
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error when deleting entry");

                ViewBag.Message = "Server mengalami masalah. Coba lagi nanti.";
                ViewBag.AlertClass = "danger";
            }

            return await Index();
        }

        [Authorize]
        public async Task<IActionResult> Save(UserEditViewModel model)
        {
            try
            {
                var entity = _mapper.Map<User>(model);
                if (model.IsEdit)
                {
                    await _userService.Update(entity);
                }
                else
                {
                    await _userService.Add(entity);
                }

                ViewBag.Message = "Perubahan telah disimpan.";
                ViewBag.AlertClass = "success";
            }
            catch (ServicesException serviceException)
            {
                ViewBag.Message = serviceException.Message;
                ViewBag.AlertClass = "warning";
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error when saving entry");

                ViewBag.Message = "Server mengalami masalah. Coba lagi nanti.";
                ViewBag.AlertClass = "danger";
            }

            return await Index();
        }
    }
}
