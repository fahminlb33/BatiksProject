using System;
using System.Threading.Tasks;
using AutoMapper;
using BatiksProject.Infrastructure;
using BatiksProject.Services;
using BatiksProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BatiksProject.Controllers
{
    public class LocalityController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger<LocalityController> _logger;
        private readonly ILocalityService _localityService;

        public LocalityController(IMapper mapper, ILocalityService localityService, ILogger<LocalityController> logger)
        {
            _mapper = mapper;
            _localityService = localityService;
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = new LocalityManageViewModel
            {
                Items = await _localityService.GetAll()
            };

            ViewBag.Sidebar = SidebarClass.LocalityManage;
            return View("Manage", model);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Sidebar = SidebarClass.LocalityAddOrEdit;
            return View("Edit", new LocalityEditViewModel { IsEdit = false });
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(int localityId)
        {
            try
            {
                var user = await _localityService.Get(localityId);
                var model = _mapper.Map<LocalityEditViewModel>(user);
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
        [HttpGet]
        public async Task<IActionResult> Delete(int localityId)
        {
            try
            {
                await _localityService.Remove(localityId);

                ViewBag.Message = "Daerah telah dihapus.";
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
        [HttpPost]
        public async Task<IActionResult> Save(LocalityEditViewModel model)
        {
            try
            {
                if (model.IsEdit)
                {
                    await _localityService.Update(model);
                }
                else
                {
                    await _localityService.Add(model);
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
