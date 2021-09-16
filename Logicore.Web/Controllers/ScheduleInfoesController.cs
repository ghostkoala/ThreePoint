using Microsoft.AspNetCore.Mvc;
using Logicore.IServices;
using System.Threading.Tasks;
using Logicore.Core.Enities;
using Logicore.Core.Quartz;
using Logicore.Web.Attributes;
using Logicore.Core.SystemConfigurationData;
using Logicore.Web.Filters;

namespace Logicore.Web.Controllers
{
    /// <summary>
    /// 任务管理
    /// </summary>
    public class ScheduleInfoesController : BaseController
    {
        private readonly IScheduleInfoService _scheduleInfoService;
        private readonly ScheduleCenter _scheduleCenter;

        public ScheduleInfoesController(IScheduleInfoService scheduleInfoService, ScheduleCenter scheduleCenter)
        {
            _scheduleInfoService = scheduleInfoService;
            _scheduleCenter = scheduleCenter;
        }

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        [Menu(Id = Menu.ScheduleId, ParentId = Menu.ScheduleId, Name = "任务管理", Order = "0")]
        public async Task<IActionResult> Index()
        {
            var info = _scheduleInfoService.GetAllAsync();
            return View(await info);
        }

        /// <summary>
        /// 创建任务页面
        /// </summary>
        /// <returns></returns>
        [ParentPermission(null, "ScheduleInfoes", "Index")]
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// 创建任务操作
        /// </summary>
        /// <param name="scheduleInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Menu(Id = Menu.ScheduleId, ParentId = Menu.ScheduleId, Name = "任务管理", Order = "1")]
        public IActionResult Create([Bind("Id,JobGroup,JobName,RunStatus,CromExpress,StarRunTime,EndRunTime,NextRunTime,Token,AppId,ServiceCode,InterfaceCode,TaskDescription,DataStatus,CreateAuthr,CreateTime")] ScheduleInfoEntity scheduleInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(scheduleInfo);
                _context.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(scheduleInfo);
        }

        /// <summary>
        /// 开始任务操作
        /// </summary>
        /// <param name="id">id</param>
        [Menu(Id = Menu.ScheduleId, ParentId = Menu.ScheduleTaskStartId, Name = "任务开始", Order = "2")]
        public void StartTask(int? id)
        {
            var info = ggb_OfflinebetaContext.ScheduleInfo.SingleOrDefault(t => t.Id == id);
            _context.AddSchedule(info);
        }

        /// <summary>
        /// 关闭任务操作
        /// </summary>
        /// <param name="id">id</param>
        [Menu(Id = Menu.ScheduleId, ParentId = Menu.ScheduleTaskStopId, Name = "任务管理", Order = "3")]
        public void StopTask(int? id)
        {
            var info = ggb_OfflinebetaContext.ScheduleInfo.SingleOrDefault(t => t.Id == id);
            _context.UpdateScheduleStatus(info);
        }

        /// <summary>
        /// 删除系统任务服务
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Menu(Id = Menu.ScheduleId, ParentId = Menu.ScheduleTaskDeleteId, Name = "任务管理", Order = "4")]
        public async Task<IActionResult> DeleteTask(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scheduleInfo = await ggb_OfflinebetaContext.ScheduleInfo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (scheduleInfo == null)
            {
                return NotFound();
            }

            return View(scheduleInfo);
        }

        /// <summary>
        /// 删除任务操作
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Menu(Id = Menu.ScheduleId, ParentId = Menu.ScheduleDeleteId, Name = "任务管理", Order = "5")]
        public async Task<IActionResult> Delete(int id)
        {
            var scheduleInfo = await ggb_OfflinebetaContext.ScheduleInfo.FindAsync(id);
            ggb_OfflinebetaContext.ScheduleInfo.Remove(scheduleInfo);
            await ggb_OfflinebetaContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}