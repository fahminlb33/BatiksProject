using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BatiksProject.Infrastructure;
using BatiksProject.Services;
using BatiksProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace BatiksProject.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ILogger<CatalogController> _logger;
        private readonly ICatalogService _catalogService;
        private readonly ILocalityService _localityService;
        private readonly IMapper _mapper;

        public CatalogController(ILogger<CatalogController> logger, ICatalogService catalogService, IMapper mapper, ILocalityService localityService)
        {
            _logger = logger;
            _catalogService = catalogService;
            _mapper = mapper;
            _localityService = localityService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = new CatalogIndexViewModel
            {
                Items =  await _catalogService.GetAll()
            };

            ViewBag.Navbar = NavbarClass.Catalog;
            return View("Index", model);
        }

        [HttpGet]
        public async Task<IActionResult> Search(SearchViewModel form)
        {
            var model = new CatalogIndexViewModel();
            if (form.UploadedFile != null)
            {
                model.Items = await _catalogService.FindByImage(form.UploadedFile);
            }
            else
            {
                model.Items = await _catalogService.FindByKeyword(form.SearchText);
            }

            ViewBag.Navbar = NavbarClass.Catalog;
            return View("Index", model);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int batikId)
        {
            var entity = await _catalogService.Get(batikId);
            var model = _mapper.Map<CatalogDetailViewModel>(entity);

            ViewBag.Navbar = NavbarClass.Catalog;
            return View("Detail", model);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Manage()
        {
            var model = new CatalogManageViewModel
            {
                Items =  await _catalogService.GetAll()
            };

            ViewBag.Sidebar = SidebarClass.BatikManage;
            return View("Manage", model);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var localities = await _localityService.GetAll();
            var model = new CatalogEditViewModel
            {
                IsEdit = false,
                Localities = localities.Select(x => new SelectListItem(x.Name, x.LocalityId.ToString()))
            };

            ViewBag.Sidebar = SidebarClass.BatikAddOrEdit;
            return View("Edit", model);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(int batikId)
        {
            try
            {
                var localities = await _localityService.GetAll();
                var batik = await _catalogService.Get(batikId);
                var model = _mapper.Map<CatalogEditViewModel>(batik);
                model.IsEdit = true;
                model.Localities = localities.Select(x => new SelectListItem(x.Name, x.LocalityId.ToString()));

                ViewBag.Sidebar = SidebarClass.BatikAddOrEdit;
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

            return await Manage();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Delete(int batikId)
        {
            try
            {
                await _catalogService.Remove(batikId);

                ViewBag.Message = "Batik telah dihapus.";
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

            return await Manage();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Save(CatalogEditViewModel model)
        {
            try
            {
                if (model.IsEdit)
                {
                    await _catalogService.Update(model);
                }
                else
                {
                    await _catalogService.Add(model);
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

            return await Manage();
        }

    }
}
