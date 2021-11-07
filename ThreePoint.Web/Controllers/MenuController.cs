using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThreePoint.IServices;
using ThreePoint.Web.Attributes;
using ThreePoint.Core.SystemConfigurationData;
using ThreePoint.Core.Enums;
using ThreePoint.Core.ServerModels;
using ThreePoint.Core.Extensions;
using ThreePoint.Core.Enities.ServiceModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using ThreePoint.Core.ViewModel;
using ThreePoint.Web.Extensions;
using ThreePoint.Core.Filters;
using ThreePoint.Web.Filters;

namespace ThreePoint.Web.Controllers
{
    /// <summary>
    /// 菜单管理
    /// </summary>
    public class MenuController : BaseController
    {
        private readonly IMenuService _menuService;

        /// <summary>
        /// ctor
        /// </summary>
        public MenuController(IMenuService menuSvc)
        {
            _menuService = menuSvc;
        }

        /// <summary>
        /// 菜单首页
        /// </summary>
        /// <returns></returns>
        [Menu(Id = Menu.MenuPageId, ParentId = Menu.SystemId, Name = "菜单管理", Order = "4", Icon = "glyphicon glyphicon-briefcase")]
        public IActionResult Index()
        {
            ViewData["title"] = "菜单管理";
            return View();
        }

        /// <summary>
        /// 添加视图
        /// </summary>
        /// <returns></returns>
        [Menu(Id = Menu.MenuAddId, ParentId = Menu.MenuPageId, Name = "添加菜单", Order = "1")]
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.ModelClose = false;
            SetTypeList(MenuType.Module);
            return View(new MenuDto());
        }

        /// <summary>
        /// 编辑视图
        /// </summary>
        /// <param name="id">用户Id</param>
        /// <returns></returns>
        [Menu(Id = Menu.MenuEditId, ParentId = Menu.MenuPageId, Name = "编辑菜单", Order = "2")]
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var model = await _menuService.FindAsync(id);
            SetTypeList(model.Type);
            return View(model);
        }

        /// <summary>
        /// awesome图标
        /// </summary>
        [AllowAnonymous]
        public IActionResult FontAwesome()
        {
            return View();
        }

        /// <summary>
        /// Glyphicons图标
        /// </summary>
        [AllowAnonymous]
        [HttpGet]
        public IActionResult FontGlyphicons()
        {
            return View();
        }

        /// <summary>
        /// 添加事件
        /// </summary>
        /// <param name="dto">数据</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(MenuDto dto)
        {
            if (ModelState.IsValid)
            {
                var result = new ResultModel<string>();
                result.Data = await _menuService.AddAsync(dto);
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
        /// 编辑事件
        /// </summary>
        /// <param name="dto">数据</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(MenuDto dto)
        {
            if (ModelState.IsValid)
            {
                var result = new ResultModel<bool>();
                result.Data = await _menuService.UpdateAsync(dto);
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
        /// 删除事件
        /// </summary>
        /// <param name="ids">ids</param>
        /// <returns></returns>
        [Menu(Id = Menu.MenuDeleteId, ParentId = Menu.MenuPageId, Name = "删除菜单", Order = "3")]
        public async Task<IActionResult> Delete(IEnumerable<string> ids)
        {
            var result = new ResultModel<bool>();
            if (ids.AnyOne())
            {
                result.Status = await _menuService.DeleteAsync(ids);
            }
            return Json(result);
        }

        /// <summary>
        /// 菜单下拉框搜索
        /// </summary>
        /// <param name="filter">过滤</param>
        /// <returns></returns>
        [HttpPost]
        [ParentPermission(null, "Menu", "Index")]
        public async Task<IActionResult> DropDownMenuSearch(DropDownMenuFilter filter)
        {
            if (filter.type == null) return null;
            if (filter.model == null) filter.model = DropDownOrSelectModel.DropDown;
            var dropDownMenuList = await _menuService.DropDownMenuSearchAsync(filter);
            ViewBag.WhatModel = (DropDownOrSelectModel)filter.model;
            return View(dropDownMenuList);
        }

        /// <summary>
        /// 表格查找菜单
        /// </summary>
        /// <param name="filters">过滤</param>
        [ParentPermission(null, "Menu", "Index")]
        public async Task<JsonResult> GetMenuForTable(MenuFilter filters)
        {
            var rows = await _menuService.GetMenuForTableAsync(filters);
            if (rows == null) return null;
            return Json(new { total = rows.records, rows = rows.rows });
        }

        private void SetTypeList(MenuType type)
        {
            var typeList = new List<SelectListItem>
            {
                new SelectListItem(MenuType.Module.GetDescriptionForEnum(),((int)MenuType.Module).ToString(), type == MenuType.Module),
                new SelectListItem(MenuType.Menu.GetDescriptionForEnum(),((int)MenuType.Menu).ToString(), type == MenuType.Menu),
                new SelectListItem(MenuType.Action.GetDescriptionForEnum(),((int)MenuType.Action).ToString(), type == MenuType.Action),
            };
            ViewBag.TypeList = typeList;
        }

    }
}