using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BatiksProject.Dto;
using BatiksProject.Infrastructure;
using BatiksProject.Models;
using BatiksProject.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BatiksProject.Services
{
    public interface ICatalogService
    {
        Task<BatikDto> Get(int batikId);
        Task<IEnumerable<BatikDto>> GetLatestBatik();
        Task<IEnumerable<BatikDto>> GetAll();
        Task<IEnumerable<BatikDto>> FindByImage(IFormFile file);
        Task<IEnumerable<BatikDto>> FindByKeyword(string keyword);
        Task<int> CountAll();

        Task Add(CatalogEditViewModel model);
        Task Update(CatalogEditViewModel model);
        Task Remove(int batikId);
    }

    public class CatalogService : ICatalogService
    {
        private readonly BatikContext _batikContext;
        private readonly IImageRetrievalService _retrievalService;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IMapper _mapper;

        public CatalogService(BatikContext batikContext, IMapper mapper, IWebHostEnvironment hostEnvironment, IImageRetrievalService retrievalService)
        {
            _batikContext = batikContext;
            _mapper = mapper;
            _hostEnvironment = hostEnvironment;
            _retrievalService = retrievalService;
        }

        public async Task<BatikDto> Get(int batikId)
        {
            var entity = await _batikContext.Batiks
                .Include(x => x.Locality)
                .SingleAsync(x => x.BatikId == batikId);

            return _mapper.Map<BatikDto>(entity);
        }

        public async Task<IEnumerable<BatikDto>> GetLatestBatik()
        {
            var list = await _batikContext.Batiks
                .Include(x => x.Locality)
                .OrderByDescending(x => x.BatikId)
                .Take(3)
                .ToListAsync();

            return _mapper.Map<List<BatikDto>>(list);
        }

        public async Task<IEnumerable<BatikDto>> GetAll()
        {
            var list = await _batikContext.Batiks
                .Include(x => x.Locality)
                .OrderByDescending(x => x.BatikId)
                .ToListAsync();

            return _mapper.Map<List<BatikDto>>(list);
        }

        public async Task<IEnumerable<BatikDto>> FindByImage(IFormFile file)
        {
            var list = await _batikContext.Batiks
                .Include(x => x.Locality)
                .OrderByDescending(x => x.BatikId)
                .ToListAsync();

            await using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            ms.Seek(0, SeekOrigin.Begin);
            
            var features = _retrievalService.Describe(ms);
            var filtered = _retrievalService.GetMostSimilar(features, list).ToList();
            
            return _mapper.Map<List<BatikDto>>(filtered);
        }

        public async Task<IEnumerable<BatikDto>> FindByKeyword(string keyword)
        {
            var list = await _batikContext.Batiks
                .Include(x => x.Locality)
                .Where(x => EF.Functions.Like(x.Title, $"%{keyword}%"))
                .ToListAsync();

            return _mapper.Map<List<BatikDto>>(list);
        }

        public async Task<int> CountAll()
        {
            return await _batikContext.Batiks.CountAsync();
        }

        public async Task Add(CatalogEditViewModel model)
        {
            try
            {
                var batik = _mapper.Map<Batik>(model);
                var savePath = await SaveFile(model.Image);

                batik.Locality = await _batikContext.Localities.FindAsync(model.LocalityId);
                batik.UploadName = Path.GetFileName(savePath);
                batik.Features = _retrievalService.Describe(savePath);

                await _batikContext.Batiks.AddAsync(batik);
                await _batikContext.SaveChangesAsync();
            }
            catch (IOException ioex)
            {
                throw new ServicesException("Gagal menyimpan file.", ioex);
            }
        }

        public async Task Update(CatalogEditViewModel model)
        {
            var batik = _mapper.Map<Batik>(model);
            _batikContext.Attach(batik);
            var entry = _batikContext.Entry(batik);

            batik.Locality = await _batikContext.Localities.FindAsync(model.LocalityId);
            if (model.Image != null)
            {
                var savePath = await SaveFile(model.Image);
                batik.UploadName = Path.GetFileName(savePath);
                batik.Features = _retrievalService.Describe(savePath);
            }
            else
            {
                entry.Property(x => x.UploadName).IsModified = false;
                entry.Property(x => x.Features).IsModified = false;
            }

            await _batikContext.SaveChangesAsync();
        }

        public async Task Remove(int batikId)
        {
            var entry = await _batikContext.Batiks.FindAsync(batikId);
            _batikContext.Batiks.Remove(entry);
            await _batikContext.SaveChangesAsync();
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var ext = Path.GetExtension(file.FileName);
            var savePath = Path.Combine( _hostEnvironment.WebRootPath, "upload", $"{Path.GetRandomFileName()}{ext}");
            await using var fs = new FileStream(savePath, FileMode.Create);
            await file.CopyToAsync(fs);
            
            return savePath;
        }
    }
}
