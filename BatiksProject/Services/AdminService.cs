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

        public AdminService(IUserService userService, ICatalogService catalogService)
        {
            _userService = userService;
            _catalogService = catalogService;
        }

        public async Task<DashboardDto> GetSummary()
        {
            return new DashboardDto
            {
                BatikCount = await _catalogService.CountAll(),
                AdminCount = await _userService.CountAll()
            };
        }
    }
}
