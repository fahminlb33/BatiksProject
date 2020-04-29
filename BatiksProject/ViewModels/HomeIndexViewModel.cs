using System.Collections.Generic;
using BatiksProject.Dto;

namespace BatiksProject.ViewModels
{
    public class HomeIndexViewModel
    {
        public IEnumerable<BatikDto> LatestBatik { get; set; }
    }
}
