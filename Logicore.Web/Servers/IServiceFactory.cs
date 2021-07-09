using System.Threading.Tasks;
using Logicore.Web.Models;

namespace Logicore.Web.Servers
{
    public interface IServiceFactory
    {
        /// <summary>
        /// 获取登陆信息
        /// </summary>
        /// <returns></returns>
        Task<AdminAuthorizeModel> GetLoginInfoAsync();

        /// <summary>
        /// 检测权限许可
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="url">url</param>
        /// <returns></returns>
        Task<bool> CheckPermitAsync(string roleId, string url);
    }
}