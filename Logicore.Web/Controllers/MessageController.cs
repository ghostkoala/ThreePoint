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
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace Logicore.Web.Controllers
{
    /// <summary>
    /// 站内信管理
    /// </summary>
    public class MessageController : BaseController
    {
        private readonly IMessageService _messageService;
        private readonly IConfiguration _configuration;
        private readonly IAdminService _adminService;

        /// <summary>
        /// ctor
        /// </summary>
        public MessageController(IMessageService messageService, IConfiguration configuration, IAdminService adminService)
        {
            _messageService = messageService;
            _configuration = configuration;
            _adminService = adminService;
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
        public IActionResult Send()
        {
            string sendToken = Guid.NewGuid().ToString("N");
            ViewData["SendMessageToken"] = sendToken;
            HttpContext.Session.SetString("SendMessageToken", sendToken);
            return View();
        }

        /// <summary>
        /// 站内信发送操作
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Send(MessageDto dto)
        {
            var sendToken = HttpContext.Session.GetString("SendMessageToken");
            string ReceiveToken = HttpContext.Request.Form["SendMessageToken"];
            if (ReceiveToken.IsBlank()) return RedirectToAction("Send");
            if (sendToken != ReceiveToken)
                return Content("<script >alert('请勿二次提交数据');window.open('" + Url.Content("/Message/Send") + "', '_self')</script >", "text/html");
            if (ModelState.IsValid)
            {
                var ok = await _messageService.SendAsync(dto);
                HttpContext.Session.Remove("SendMessageToken");
                if (ok)
                    return Content("<script >alert('发送成功！');window.open('" + Url.Content("/Message/Send") + "', '_self')</script >", "text/html");
                else
                    return Content("<script >alert('发送失败！');window.open('" + Url.Content("/Message/Send") + "', '_self')</script >", "text/html");
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
                var ok = await _messageService.EditAsync(dto);
                if (ok)
                    return Content("<script >alert('修改成功！');parent.window.nthTabs.delTab();</script >", "text/html");
                else
                    return Content("<script >alert('修改失败！');window.open('" + Url.Content("/Message/Edit/" + dto.Id) + "', '_self')</script >", "text/html");
            }
            return View();
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

        [ParentPermission(null, "Message", "Index")]
        public async Task<JsonResult> GetMessageForTable(MessageFilter filter)
        {
            var rows = await _messageService.GetMessageForTableAsync(filter);
            if (rows == null) return null;
            return Json(new { total = rows.records, rows = rows.rows });
        }

        [ParentPermission(null, "Message", "Index")]
        public async Task<IActionResult> GetMessageDetails(string id)
        {
            var result = await _messageService.GetMessageDetails(id);
            return View(result);
        }

        [NonePermission]
        public async Task<IActionResult> ReadMessage(string id)
        {
            // var goldList = _configuration.GetValue<string>("GoldList", "").Split(',');
            // var userId = HttpContext.Session.GetString("Uid");
            // var admin = await _adminService.FindAsync(userId);
            // var isSupper = goldList.Any(x => x == admin.LoginName);
            var message = await _messageService.ReadMessageAsync(id);
            if (message == null) return Content("没有要阅读的信息");
            return View(message);
        }
    }
}