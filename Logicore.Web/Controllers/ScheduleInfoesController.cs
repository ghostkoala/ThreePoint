using Microsoft.AspNetCore.Mvc;
using Logicore.IServices;
using System.Threading.Tasks;

namespace Logicore.Web.Controllers
{
    public class ScheduleInfoesController : BaseController
    {
        private readonly IScheduleInfoService _scheduleInfoService;

        public ScheduleInfoesController(IScheduleInfoService scheduleInfoService)
        {
            _scheduleInfoService = scheduleInfoService;
        }

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var info = _context.GetAllScheduleList().ToListAsync();
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
        public IActionResult Create([Bind("Id,JobGroup,JobName,RunStatus,CromExpress,StarRunTime,EndRunTime,NextRunTime,Token,AppId,ServiceCode,InterfaceCode,TaskDescription,DataStatus,CreateAuthr,CreateTime")] ScheduleInfo scheduleInfo)
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
        /// <param name="id"></param>
        public void StartTask(int? id)
        {
            var info = ggb_OfflinebetaContext.ScheduleInfo.SingleOrDefault(t => t.Id == id);
            _context.AddSchedule(info);
        }

        /// <summary>
        /// 关闭任务操作
        /// </summary>
        /// <param name="id"></param>
        public void StopTask(int? id)
        {
            var info = ggb_OfflinebetaContext.ScheduleInfo.SingleOrDefault(t => t.Id == id);
            _context.UpdateScheduleStatus(info);
        }

        /// <summary>
        /// 删除任务页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int? id)
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
        public async Task<IActionResult> Delete(int id)
        {
            var scheduleInfo = await ggb_OfflinebetaContext.ScheduleInfo.FindAsync(id);
            ggb_OfflinebetaContext.ScheduleInfo.Remove(scheduleInfo);
            await ggb_OfflinebetaContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}