using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BatiksProject.Dto;

namespace BatiksProject.Services
{
    public interface ICatalogService
    {
        Task<IEnumerable<BatikDto>> GetLatestBatik();
        Task<IEnumerable<BatikDto>> GetAll();
    }

    public class CatalogService
    {

    }
}
