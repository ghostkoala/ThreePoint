using System.Threading.Tasks;
using ThreePoint.Core.Filters;
using ThreePoint.Core.ServerModels;
using ThreePoint.Core.SystemConfigurationData;
using ThreePoint.IServices;
using ThreePoint.Web.Attributes;
using ThreePoint.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using ThreePoint.Core.Extensions;
using ThreePoint.Core.Enities.ServiceModel;
using System.Collections.Generic;
using ThreePoint.Web.Extensions;

namespace ThreePoint.Web.Controllers
{
    /// <summary>
    /// 部门管理
    /// </summary>
    public class DepartmentController : BaseController
    {
        private readonly IDepartmentService _departmentService;

        /// <summary>
        /// ctor
        /// </summary>
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }


        /// <summary>
        /// 部门下拉框搜索
        /// </summary>
        /// <param name="filter">过滤</param>
        /// <returns></returns>
        [ParentPermission(null, "Department", "Index")]
        [ParentPermission(null, "Admin", "Add")]
        [ParentPermission(null, "Admin", "Edit")]
        [HttpPost]
        public async Task<IActionResult> DropDownDepartmentSearch(DropDownDepartmentFilter filter)
        {
            if (filter.model == null) return null;
            var dropDownDepartmentuList = await _departmentService.DropDownDepartmentSeachAsync(filter);
            ViewBag.WhatModel = filter.model;
            return View(dropDownDepartmentuList);
        }

        /// <summary>
        /// 部门管理首页
        /// </summary>
        /// <returns></returns>
        [Menu(Id = Menu.DepartmentPageId, ParentId = Menu.SystemId, Name = "部门管理", Order = "0", Icon = "glyphicon glyphicon-list-alt")]
        public IActionResult Index()
        {
            return View();
        }

        [ParentPermission(null, "Department", "Index")]
        public async Task<IActionResult> GetDepartmentForTable(DepartmentFilter filter)
        {
            var rows = await _departmentService.GetDepartmentForTableAsync(filter);
            if (rows == null) return null;
            return Json(new { total = rows.records, rows = rows.rows });
        }

        /// <summary>
        /// 添加部门视图
        /// </summary>
        /// <returns></returns>
        [Menu(Id = Menu.DepartmentAddId, ParentId = Menu.DepartmentPageId, Name = "部门管理", Order = "1")]
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(DepartmentDto dto)
        {
            if (ModelState.IsValid)
            {
                var result = new ResultModel<bool>();
                result.Data = await _departmentService.AddAsync(dto);
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
        /// 修改部门视图
        /// </summary>
        /// <returns></returns>
        [Menu(Id = Menu.DepartmentEditId, ParentId = Menu.DepartmentPageId, Name = "修改部门", Order = "2")]
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var dto = await _departmentService.FindAsync(id);
            return View(dto);
        }

        /// <summary>
        /// 修改部门操作
        /// </summary>
        /// <param name="">数据</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(DepartmentDto dto)
        {
            if (ModelState.IsValid)
            {
                var result = new ResultModel<bool>();
                result.Data = await _departmentService.EditAsync(dto);
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
        /// 删除部门操作
        /// </summary>
        /// <param name="ids">ids</param>
        /// <returns></returns>
        [Menu(Id = Menu.DepartmentDeleteId, ParentId = Menu.DepartmentPageId, Name = "删除部门", Order = "3")]
        [HttpPost]
        public async Task<IActionResult> Delete(IEnumerable<string> ids)
        {
            var result = new ResultModel<bool>();
            if (ids.AnyOne())
            {
                result.Status = await _departmentService.DeleteAsync(ids);
            }
            return Json(result);
        }

        [ParentPermission(null, "Department", "Edit")]
        [ParentPermission(null, "Department", "Add")]
        public async Task<IActionResult> GetSearchNameForAreaText(string keyWord)
        {
            if (keyWord.IsBlank()) return Content("没有数据");
            var result = (await _departmentService.GetSearchNameForAreaTextAsync(keyWord));
            if (result.Count <= 0) return Content("没有搜索到数据");
            string str = "";
            foreach (var item in result)
            {
                str = item.Key + " - " + item.Value + "\n";
            }
            return Content(str);
        }
    }
}