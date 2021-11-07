using System.Threading.Tasks;
using ThreePoint.IServices;
using ThreePoint.Web.Models;
using Microsoft.AspNetCore.Http;

namespace ThreePoint.Web.Servers
{
    public class ServiceFactory : IServiceFactory
    {
        public string Name { get; set; }
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAdminService _adminService;

        public ServiceFactory(IHttpContextAccessor httpContextAccessor,
        IAdminService adminService)
        {
            _httpContextAccessor = httpContextAccessor;
            _adminService = adminService;
        }

        public async Task<AdminAuthorizeModel> GetLoginInfoAsync()
        {
            var uId = _httpContextAccessor.HttpContext.Session.GetString("Uid");
            if (uId == string.Empty || uId == null) { return null; }
            var user = await _adminService.FindAsync(uId);
            //AdminAuthorizeModel u = user;
            return user;
        }

        public Task<bool> CheckPermitAsync(string roleId, string url)
        {
            return _adminService.CheckPermitAsync(roleId, url);
        }
    }
}