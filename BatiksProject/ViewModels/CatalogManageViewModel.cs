using System.Collections.Generic;
using BatiksProject.Dto;

namespace BatiksProject.ViewModels
{
    public class CatalogManageViewModel
    {
        public IEnumerable<BatikDto> Items { get; set; }
    }
}
