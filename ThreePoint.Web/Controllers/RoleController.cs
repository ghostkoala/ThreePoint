using System.Collections.Generic;
using System.Threading.Tasks;
using ThreePoint.Core.Enities.ServiceModel;
using ThreePoint.Core.Extensions;
using ThreePoint.Core.Filters;
using ThreePoint.Core.ServerModels;
using ThreePoint.Core.SystemConfigurationData;
using ThreePoint.IServices;
using ThreePoint.Web.Attributes;
using ThreePoint.Web.Extensions;
using ThreePoint.Web.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ThreePoint.Web.Controllers
{
    /// <summary>
    /// 角色管理
    /// </summary>
    public class RoleController : BaseController
    {
        private readonly IRoleService _roleService;

        /// <summary>
        /// ctor
        /// </summary>
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        /// <summary>
        /// 角色下拉框搜索
        /// </summary>
        /// <param name="filter">过滤</param>
        /// <returns></returns>
        [HttpPost]
        [ParentPermission(null, "Admin", "Index")]
        [ParentPermission(null, "Role", "Index")]
        public async Task<IActionResult> DropDownRoleSearch(DropDownRoleFilter filter)
        {
            if (filter.model == null) return null;
            var dropDownMenuList = await _roleService.DropDownRoleSearchAsync(filter);
            ViewBag.WhatModel = filter.model;
            return View(dropDownMenuList);
        }

        /// <summary>
        /// 角色管理主页
        /// </summary>
        /// <returns></returns>
        [Menu(Id = Menu.RolePageId, ParentId = Menu.SystemId, Name = "角色管理", Order = "0", Icon = "glyphicon glyphicon-lock")]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 表格查找-角色
        /// </summary>
        /// <param name="filter">过滤</param>
        /// <returns></returns>
        [ParentPermission(null, "Role", "Index")]
        public async Task<IActionResult> GetRoleForTableAsync(RoleFilter filter)
        {
            var rows = await _roleService.GetRoleForTableAsync(filter);
            if (rows == null) return null;
            return Json(new { total = rows.records, rows = rows.rows });
        }

        /// <summary>
        /// 添加角色页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// 添加角色操作
        /// </summary>
        /// <param name="dto">数据</param>
        /// <returns></returns>
        [Menu(Id = Menu.RoleAddId, ParentId = Menu.RolePageId, Name = "添加角色", Order = "1")]
        [HttpPost]
        public async Task<IActionResult> AddAsync(RoleDto dto)
        {
            if (ModelState.IsValid)
            {
                var result = new ResultModel<string>();
                result.Data = await _roleService.AddAsync(dto);
                if (result.Data.IsNotBlank()) result.Status = true;
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
        /// 修改角色页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> EditAsync(string id)
        {
            if (id.IsBlank()) return null;
            var result = await _roleService.FindAsync(id);
            return View(result);
        }

        /// <summary>
        /// 修改角色操作
        /// </summary>
        /// <param name="dto">数据</param>
        /// <returns></returns>
        [Menu(Id = Menu.RoleEditId, ParentId = Menu.RolePageId, Name = "修改角色", Order = "2")]
        [HttpPost]
        public async Task<IActionResult> EditAsync(RoleDto dto)
        {
            if (ModelState.IsValid)
            {
                var result = new ResultModel<bool>();
                result.Data = await _roleService.EditAsync(dto);
                result.Status = result.Data;
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
        /// 删除角色操作
        /// </summary>
        /// <param name="ids">角色Ids</param>
        /// <returns></returns>
        [Menu(Id = Menu.RoleDeleteId, ParentId = Menu.RolePageId, Name = "删除角色", Order = "3")]
        [HttpPost]
        public async Task<IActionResult> Delete(IEnumerable<string> ids)
        {
            var result = new ResultModel<bool>();
            if (ids.AnyOne())
            {
                result.Status = await _roleService.DeleteAsync(ids);
            }
            return Json(result);
        }

        /// <summary>
        /// 修改限权树视图
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns></returns>
        [Menu(Id = Menu.EditPermissionWithRoleId, ParentId = Menu.RolePageId, Name = "修改角色限权", Order = "4")]
        [HttpGet]
        public async Task<IActionResult> EditPermissionWithRole(string id)
        {
            ViewBag.ZtreeModel = await _roleService.GetPermissionTreeAsync(id);
            ViewBag.roleId = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditPermissionWithRole(string roleId, IEnumerable<string> ids)
        {
            var result = new ResultModel<bool>();
            if (ids.AnyOne() && roleId.IsNotBlank())
            {
                result.Status = await _roleService.EditPermissionWithRoleAsync(roleId, ids);
            }
            return Json(result);
        }
    }
}