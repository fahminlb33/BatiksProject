using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BatiksProject.Dto;
using BatiksProject.Models;
using BatiksProject.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BatiksProject.Services
{
    public interface ICatalogService
    {
        Task<IEnumerable<BatikDto>> GetLatestBatik();
        Task<IEnumerable<BatikDto>> GetAll();
        Task<int> CountAll();
    }

    public class CatalogService : ICatalogService
    {
        private readonly BatikContext _batikContext;
        private readonly IMapper _mapper;

        public CatalogService(BatikContext batikContext, IMapper mapper)
        {
            _batikContext = batikContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BatikDto>> GetLatestBatik()
        {
            var list = await _batikContext.Batiks.OrderByDescending(x => x.BatikId).Take(3).ToListAsync();
            return _mapper.Map<List<Batik>, List<BatikDto>>(list);
        }

        public async Task<IEnumerable<BatikDto>> GetAll()
        {
            var list = await _batikContext.Batiks.OrderByDescending(x => x.BatikId).ToListAsync();
            return _mapper.Map<List<Batik>, List<BatikDto>>(list);
        }

        public async Task<int> CountAll()
        {
            return await _batikContext.Batiks.CountAsync();
        }
    }
}
