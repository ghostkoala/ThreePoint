using System.Threading.Tasks;
using Logicore.Core.Filters;
using Logicore.Core.SystemConfigurationData;
using Logicore.IServices;
using Logicore.Web.Attributes;
using Logicore.Web.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Logicore.Web.Controllers
{
    public class ServerExceptionController : BaseController
    {
        private readonly IServerExceptionService _serverExceptionService;

        public ServerExceptionController(IServerExceptionService serverExceptionService)
        {
            _serverExceptionService = serverExceptionService;
        }

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        [Menu(Id = Menu.ServerExceptionPageId, ParentId = Menu.LogsId, Name = "系统异常日志", Order = "0")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [ParentPermission(null, "ServerException", "Index")]
        public async Task<JsonResult> GetServerExceptionForTable(ServerExceptionFilter filter)
        {
            var rows = await _serverExceptionService.GetServerExceptionForTable(filter);
            if (rows == null) return null;
            return Json(new { total = rows.records, rows = rows.rows });
        }
    }
}