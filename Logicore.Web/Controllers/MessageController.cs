using System.Threading.Tasks;
using Logicore.Core.ServerModels;
using Logicore.Core.SystemConfigurationData;
using Logicore.IServices;
using Logicore.Web.Attributes;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Logicore.Core.Enities.ServiceModel;
using Logicore.Web.Extensions;
using Logicore.Core.Extensions;
using Logicore.Web.Filters;
using Logicore.Core.Filters;

namespace Logicore.Web.Controllers
{
    /// <summary>
    /// 站内信管理
    /// </summary>
    public class MessageController : BaseController
    {
        private readonly IMessageService _messageService;

        /// <summary>
        /// ctor
        /// </summary>
        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        [Menu(Id = Menu.MessagePageId, ParentId = Menu.SystemId, Name = "站内信管理", Order = "0", Icon = "glyphicon glyphicon-envelope")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 站内信发送
        /// </summary>
        /// <returns></returns>
        [Menu(Id = Menu.MessageSendId, ParentId = Menu.MessagePageId, Name = "站内信发送", Order = "1")]
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Send()
        {
            return View();
        }

        /// <summary>
        /// 站内信发送操作
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Send(MessageDto dto)
        {
            var ok = false;
            if (ModelState.IsValid)
            {
                ok = await _messageService.SendAsync(dto);
                return Content("<script >alert('发送成功！');window.open('" + Url.Content("/Message/Send") + "', '_self')</script >", "text/html");
            }
            return View();
        }

        /// <summary>
        /// 站内信修改页
        /// </summary>
        /// <param name="id">站内信Id</param>
        /// <returns></returns>
        [Menu(Id = Menu.MessageEditId, ParentId = Menu.MessagePageId, Name = "站内信修改", Order = "2")]
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var dto = await _messageService.FindAsync(id);
            return View(dto);
        }

        /// <summary>
        /// 站内信修改操作
        /// </summary>
        /// <param name="dto">数据</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(MessageDto dto)
        {
            if (ModelState.IsValid)
            {
                var result = new ResultModel<bool>();
                result.Data = await _messageService.EditAsync(dto);
                if (result.Data) result.Status = true;
                return Json(result);
            }
            else
            {
                //找到出错的字段以及出错信息
                var modelErrors = this.ExpendErrors();
                var result = new ResultModel<List<string>>(410, false, "输入的数据有误", modelErrors);
                return Json(result);
            }
        }

        /// <summary>
        /// 站内信删除
        /// </summary>
        /// <param name="ids">ids</param>
        /// <returns></returns>
        [Menu(Id = Menu.MessageDeleteId, ParentId = Menu.MessagePageId, Name = "站内信修改", Order = "3")]
        [HttpPost]
        public async Task<IActionResult> Delete(IEnumerable<string> ids)
        {
            var result = new ResultModel<bool>();
            if (ids.AnyOne())
            {
                result.Status = await _messageService.DeleteAsync(ids);
            }
            return Json(result);
        }

        [ParentPermission(null, "Admin", "Index")]
        public async Task<JsonResult> GetMessageForTable(MessageFilter filter)
        {
            var rows = await _messageService.GetMessageForTableAsync(filter);
            if (rows == null) return null;
            return Json(new { total = rows.records, rows = rows.rows });
        }
    }
}