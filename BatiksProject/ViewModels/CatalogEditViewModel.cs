using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BatiksProject.ViewModels
{
    public class CatalogEditViewModel
    {
        public bool IsEdit { get;set; }

        public int BatikId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int LocalityId { get; set; }

        public string UploadName { get; set; }

        public IFormFile Image { get; set; }

        public IEnumerable<SelectListItem> Localities { get; set; }
    }
}
