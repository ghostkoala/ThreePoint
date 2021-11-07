using Microsoft.AspNetCore.Mvc;
using ThreePoint.Core.ServerModels;
using ThreePoint.IServices;
using ThreePoint.Web.Attributes;
using ThreePoint.Core.SystemConfigurationData;
using System.Threading.Tasks;

namespace ThreePoint.Web.Controllers
{
    public class SystemConfigController : BaseController
    {
        private readonly ISystemConfigService _systemConfigService;

        /// <summary>
        /// ctor
        /// </summary>
        public SystemConfigController(ISystemConfigService systemConfigService)
        {
            _systemConfigService = systemConfigService;
        }

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        [Menu(Id = Menu.ServerConfigPageId, ParentId = Menu.SystemId, Name = "系统信息详情", Order = "0")]
        public async Task<IActionResult> Index()
        {
            var result = await _systemConfigService.GetAsync();
            return View(result);
        }

        /// <summary>
        /// 更新系统信息页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Menu(Id = Menu.ServerConfigEditId, ParentId = Menu.ServerConfigPageId, Name = "更新系统信息", Order = "１")]
        public async Task<IActionResult> Edit()
        {
            var result = await _systemConfigService.GetAsync();
            SystemConfigDto dto = new SystemConfigDto()
            {
                SystemName = result.SystemName
            };
            return View(dto);
        }

        /// <summary>
        /// 更新系统信息操作
        /// </summary>
        /// <param name="dto">数据</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(SystemConfigDto dto)
        {
            var ok = await _systemConfigService.EditAsync(dto);
            if (ok)
            { return RedirectToAction("Index"); }
            else
            { return Content("<h4>修改失败！请重试！！</h4>"); }
        }
    }
}