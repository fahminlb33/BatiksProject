using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BatiksProject.Dto;
using BatiksProject.Infrastructure;
using BatiksProject.Models;
using BatiksProject.ViewModels;
using EntityFramework.Exceptions.Common;
using Microsoft.EntityFrameworkCore;

namespace BatiksProject.Services
{
    public interface ILocalityService
    {
        Task<IEnumerable<LocalityDto>> GetAll();
        Task<LocalityDto> Get(int localityId);
        Task<int> CountAll();

        Task Add(LocalityEditViewModel model);
        Task Update(LocalityEditViewModel model);
        Task Remove(int localityId);
    }

    public class LocalityService : ILocalityService
    {
        private readonly IMapper _mapper;
        private readonly BatikContext _batikContext;

        public LocalityService(BatikContext batikContext, IMapper mapper)
        {
            _batikContext = batikContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LocalityDto>> GetAll()
        {
            var entity = await _batikContext.Localities.ToListAsync();
            return _mapper.Map<List<LocalityDto>>(entity);
        }

        public async Task<LocalityDto> Get(int localityId)
        {
            var entity = await _batikContext.Localities.FindAsync(localityId);
            return _mapper.Map<LocalityDto>(entity);
        }

        public async Task<int> CountAll()
        {
            return await _batikContext.Localities.CountAsync();
        }

        public async Task Add(LocalityEditViewModel model)
        {
            try
            {
                var locality = _mapper.Map<Locality>(model);
                await _batikContext.Localities.AddAsync(locality);
                await _batikContext.SaveChangesAsync();
            }
            catch (UniqueConstraintException)
            {
                throw new ServicesException("Daerah dengan nama yang sama sudah terdaftar.");
            }
        }

        public async Task Update(LocalityEditViewModel model)
        {
            try
            {
                var locality = _mapper.Map<Locality>(model);
                _batikContext.Attach(locality);
                _batikContext.Entry(locality).State = EntityState.Modified;
                await _batikContext.SaveChangesAsync();
            }
            catch (UniqueConstraintException)
            {
                throw new ServicesException("Daerah dengan nama yang sama sudah terdaftar.");
            }
        }

        public async Task Remove(int localityId)
        {
            var entry = await _batikContext.Localities.FindAsync(localityId);
            _batikContext.Localities.Remove(entry);
            await _batikContext.SaveChangesAsync();
        }
    }
}
