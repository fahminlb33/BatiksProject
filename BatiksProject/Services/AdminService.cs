using System.Threading.Tasks;
using BatiksProject.Dto;

namespace BatiksProject.Services
{
    public interface IAdminService
    {
        Task<DashboardDto> GetSummary();
    }

    public class AdminService : IAdminService
    {
        private readonly IUserService _userService;
        private readonly ICatalogService _catalogService;
        private readonly ILocalityService _localityService;

        public AdminService(IUserService userService, ICatalogService catalogService, ILocalityService localityService)
        {
            _userService = userService;
            _catalogService = catalogService;
            _localityService = localityService;
        }

        public async Task<DashboardDto> GetSummary()
        {
            return new DashboardDto
            {
                BatikCount = await _catalogService.CountAll(),
                AdminCount = await _userService.CountAll(),
                LocalityCount = await _localityService.CountAll()
            };
        }
    }
}
