using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BatiksProject.Dto;

namespace BatiksProject.ViewModels
{
    public class CatalogIndexViewModel
    {
        public IEnumerable<BatikDto> Items { get; set; }
    }
}
