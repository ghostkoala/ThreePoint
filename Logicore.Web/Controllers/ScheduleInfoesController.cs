using Microsoft.AspNetCore.Mvc;
using Logicore.IServices;
using System.Threading.Tasks;
using Logicore.Core.Enities;
using Logicore.Core.Quartz;
using Logicore.Web.Attributes;
using Logicore.Core.SystemConfigurationData;
using Logicore.Web.Filters;
using Logicore.Core.Extensions;
using Logicore.Core.Enums;

namespace Logicore.Web.Controllers
{
    /// <summary>
    /// 任务管理
    /// </summary>
    public class ScheduleInfoesController : BaseController
    {
        private readonly IScheduleInfoService _scheduleInfoService;
        private IScheduleCenter _scheduleCenter;

        public ScheduleInfoesController(IScheduleInfoService scheduleInfoService, IScheduleCenter scheduleCenter)
        {
            _scheduleInfoService = scheduleInfoService;
            _scheduleCenter = scheduleCenter;
        }

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        [Menu(Id = Menu.SchedulePageId, ParentId = Menu.ScheduleId, Name = "任务管理", Order = "0")]
        public async Task<IActionResult> Index()
        {
            var info = _scheduleInfoService.GetAllAsync();
            return View(await info);
        }

        /// <summary>
        /// 创建任务页面
        /// </summary>
        /// <returns></returns>
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
        [Menu(Id = Menu.ScheduleCreateId, ParentId = Menu.SchedulePageId, Name = "任务管理", Order = "1")]
        public async Task<IActionResult> Create([Bind("Id,JobGroup,JobName,RunStatus,CromExpress,StarRunTime,EndRunTime,NextRunTime,Token,AppId,ServiceCode,InterfaceCode,TaskDescription,DataStatus,CreateAuthr,CreateTime")] ScheduleInfoEntity scheduleInfo)
        {
            if (ModelState.IsValid)
            {
                await _scheduleInfoService.CreateAsync(scheduleInfo);
                //如任务需要马上开启，将开启任务
                if (scheduleInfo.StarRunTime != null && scheduleInfo.StarRunTime < System.DateTime.Now)
                    await _scheduleCenter.AddJobAsync(scheduleInfo.JobName, scheduleInfo.JobGroup, null, scheduleInfo.CromExpress, null, scheduleInfo.StarRunTime, scheduleInfo.EndRunTime, scheduleInfo.TaskDescription);

                return RedirectToAction(nameof(Index));
            }
            return View(scheduleInfo);
        }

        /// <summary>
        /// 开始任务操作
        /// </summary>
        /// <param name="id">id</param>
        [Menu(Id = Menu.ScheduleTaskStartId, ParentId = Menu.SchedulePageId, Name = "任务开始", Order = "2")]
        public async Task<IActionResult> StartTask(string id)
        {
            if (id.IsBlank()) return NotFound();
            var info = await _scheduleInfoService.FindAsync(id);
            var status = await _scheduleCenter.CheckStatusAsync(info.JobName, info.JobGroup);
            return Ok();
        }

        /// <summary>
        /// 关闭任务操作
        /// </summary>
        /// <param name="id">id</param>
        [Menu(Id = Menu.ScheduleTaskStopId, ParentId = Menu.SchedulePageId, Name = "任务管理", Order = "3")]
        public async Task<IActionResult> StopTask(string id)
        {
            var entity = await _scheduleInfoService.FindAsync(id);
            if (entity == null) return NotFound();
            var result = _scheduleCenter.StopJobAsync(entity.JobName, entity.JobGroup);

            if (id.IsBlank()) return NotFound();
            var info = await _scheduleInfoService.FindAsync(id);
            var ok = await _scheduleCenter.StopJobAsync(info.JobName, info.JobGroup);
            if (ok == null)
            {
                await _scheduleInfoService.UpdataStatus(id, ((int)ScheduleStatus.Canceled));
                return Ok();
            }
            else
            {
                return Content("停止任务失败！");
            }
        }

        /// <summary>
        /// 系统任务详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Menu(Id = Menu.ScheduleDetailId, ParentId = Menu.SchedulePageId, Name = "任务管理", Order = "4")]
        [ParentPermissionAttribute(null, "ScheduleInfoes", "Index")]
        public async Task<IActionResult> Details(string id)
        {
            if (id.IsBlank())
            {
                return NotFound();
            }

            var scheduleInfo = await _scheduleInfoService.FindAsync(id);
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
        [Menu(Id = Menu.ScheduleDeleteId, ParentId = Menu.SchedulePageId, Name = "任务管理", Order = "5")]
        public async Task<IActionResult> Delete(string id)
        {
            var entity = await _scheduleInfoService.FindAsync(id);
            if (entity == null) return NotFound();
            var scheduleInfo = await _scheduleInfoService.DeleteAsync(id);
            if (!scheduleInfo) return Content("数据删除失败，请重试");
            await _scheduleCenter.DeleteJobAsync(entity.JobName, entity.JobGroup);
            return Content("删除成功！");
        }
    }
}