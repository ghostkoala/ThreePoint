using Microsoft.AspNetCore.Mvc;
using Logicore.IServices;
using System.Threading.Tasks;
using Logicore.Web.Attributes;
using Logicore.Core.SystemConfigurationData;
using Logicore.Core.ViewModel;
using Logicore.Core.Enities.ServiceModel;
using Logicore.Web.Extensions;
using Logicore.Core.ServerModels;
using Logicore.Core.Extensions;
using System.Collections.Generic;
using Logicore.Core.Filters;
using Microsoft.AspNetCore.Http;
using Logicore.Web.Filters;

namespace Logicore.Web.Controllers
{
    /// <summary>
    /// 管理员管理
    /// </summary>
    public class AdminController : BaseController
    {
        private readonly IAdminService _adminService;
        private readonly IDepartmentService _departmentService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRoleService _roleService;

        /// <summary>
        /// ctor
        /// </summary>
        public AdminController(IAdminService adminService,
        IDepartmentService departmentService,
        IHttpContextAccessor httpContextAccessor,
        IRoleService roleService)
        {
            _adminService = adminService;
            _departmentService = departmentService;
            _httpContextAccessor = httpContextAccessor;
            _roleService = roleService;
        }

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        [Menu(Id = Menu.AdminPageId, ParentId = Menu.SystemId, Name = "管理员管理", Order = "4")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 修改现登陆管理员密码页
        /// </summary>
        /// <returns></returns>
        [NonePermission]
        [HttpGet]
        public IActionResult EditPassword()
        {
            return View();
        }

        /// <summary>
        /// 修改现登陆管理员基本信息页
        /// </summary>
        /// <returns></returns>
        [NonePermission]
        [HttpGet]
        public async Task<IActionResult> EditGeneralInfo()
        {
            string id = _httpContextAccessor.HttpContext.Session.GetString("Uid");
            var info = await _adminService.FindGeneralInfoAsync(id);
            return View(info);
        }

        /// <summary>
        /// 修改现登陆管理员基本信息操作
        /// </summary>
        /// <param name="dto">数据</param>
        /// <returns></returns>
        [NonePermission]
        [HttpPost]
        [Menu(Id = Menu.AdminEditGeneralInfoId, ParentId = Menu.AdminPageId, Name = "管理员基本信息修改", Order = "1")]
        public async Task<IActionResult> EditGeneralInfo(AdminGeneralInfoDto dto)
        {
            dto.Id = _httpContextAccessor.HttpContext.Session.GetString("Uid");
            if (ModelState.IsValid)
            {
                var result = new ResultModel<bool>();
                result.Data = await _adminService.EditGeneralInfoAsync(dto);
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
        /// 修改现登陆管理员密码操作
        /// </summary>
        /// <param name="dto">数据</param>
        /// <returns></returns>
        [NonePermission]
        [HttpPost]
        [Menu(Id = Menu.AdminEditPassWordId, ParentId = Menu.AdminPageId, Name = "管理员密码修改", Order = "2")]
        public async Task<IActionResult> EditPassword(AdminEditPassWordDto dto)
        {
            if (dto.NewPassWord != dto.ConfirmPassword)
                return Json(new ResultModel<bool>(403, false, "两次输入密码不一致", false));

            dto.Id = HttpContext.Session.GetString("Uid");

            if (ModelState.IsValid)
            {
                var result = new ResultModel<bool>();
                result.Data = await _adminService.EditPassWord(dto);
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
        /// 删除管理员事件
        /// </summary>
        /// <param name="ids">Ids</param>
        /// <returns></returns>
        [Menu(Id = Menu.AdminDeleteId, ParentId = Menu.AdminPageId, Name = "删除管理员", Order = "3", Icon = "glyphicon glyphicon-user")]
        [HttpPost]
        public async Task<IActionResult> Delete(IEnumerable<string> ids)
        {
            var result = new ResultModel<bool>();
            if (ids.AnyOne())
            {
                result.Status = await _adminService.DeleteAsync(ids);
            }
            return Json(result);
        }

        /// <summary>
        /// 表格查找管理员
        /// </summary>
        /// <param name="filters">过滤</param>
        [ParentPermission(null, "Admin", "Index")]
        public async Task<JsonResult> GetAdminForTableAsync(AdminFilter filter)
        {
            var rows = await _adminService.GetAdminForTableAsync(filter);
            if (rows == null) return null;
            return Json(new { total = rows.records, rows = rows.rows });
        }

        /// <summary>
        /// 修改管理员页面
        /// </summary>
        /// <param name="id">要修改的管理员Id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var dto = await _adminService.FindAsync(id);
            ViewBag.departmentList = await _departmentService.DropDownDepartmentSeachAsync(new DropDownDepartmentFilter());
            ViewBag.roleList = await _roleService.DropDownRoleSearchAsync(new DropDownRoleFilter());
            return View(dto);
        }

        /// <summary>
        /// 修改管理员操作
        /// </summary>
        /// <param name="dto">数据</param>
        /// <returns></returns>
        [HttpPost]
        [Menu(Id = Menu.AdminEditId, ParentId = Menu.AdminPageId, Name = "修改管理员", Order = "4")]
        public async Task<JsonResult> Edit(AdminDto dto)
        {
            if (ModelState.IsValid)
            {
                var result = new ResultModel<bool>();
                result.Data = await _adminService.EditAsync(dto);
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
        /// 添加管理员页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            ViewBag.departmentList = await _departmentService.DropDownDepartmentSeachAsync(new DropDownDepartmentFilter());
            ViewBag.roleList = await _roleService.DropDownRoleSearchAsync(new DropDownRoleFilter());
            return View();
        }

        /// <summary>
        /// 添加管理员操作
        /// </summary>
        /// <param name="dto">数据</param>
        /// <returns></returns>
        [HttpPost]
        [Menu(Id = Menu.AdminAddId, ParentId = Menu.AdminPageId, Name = "添加管理员", Order = "5")]
        public async Task<JsonResult> add(AdminDto dto)
        {
            if (ModelState.IsValid)
            {
                var result = new ResultModel<bool>();
                result.Data = await _adminService.AddAsync(dto);
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

        [ParentPermission(null, "Message", "Send")]
        public async Task<IActionResult> DropDownAdminSearch(DropDownAdminFilter filter)
        {
            if (filter.model == null) return null;
            ViewBag.WhatModel = filter.model;
            var list = await _adminService.DropDownAdminSeachAsync(filter);
            return View(list);
        }
    }
}