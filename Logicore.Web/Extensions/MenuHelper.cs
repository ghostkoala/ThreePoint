using Logicore.Core.Exceptions;
using Logicore.Core.ServerModels;
using Logicore.Core.SystemConfigurationData;
using Logicore.Web.Attributes;
using Logicore.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Logicore.Web.Extensions
{
    public class MenuHelper
    {
        /// <summary>
        /// 获取所有的菜单配置
        /// </summary>
        /// <returns></returns>
        public static List<MenuDto> GetMenues()
        {
            var menus = new List<MenuDto>
            {
                new MenuDto{Id = Menu.System.Id,Name = Menu.System.Name,Icon = "glyphicon glyphicon-cog"},
                new MenuDto{Id = Menu.Logs.Id,Name = Menu.Logs.Name,Icon = "glyphicon glyphicon-menu-hamburger"},
                new MenuDto{Id = Menu.Pages.Id,Name = Menu.Pages.Name,Icon = "glyphicon glyphicon-file"}
            };

            //获取所有的控制器
            var controllers =
                from type in typeof(Startup).Assembly.GetTypes()
                where type.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase)
                select type;
            //遍历所有控制器下面定义了MenuAttribute属性的Action
            foreach (var controller in controllers)
            {
                var controllerName = controller.Name.Replace("Controller", string.Empty);

                var members = controller.GetMembers().Where(x => x.IsDefined(typeof(MenuAttribute)));

                foreach (var action in members)
                {
                    var attr = action.GetCustomAttributes<MenuAttribute>().FirstOrDefault();
                    var actionName = action.Name;

                    var menu = new MenuDto
                    {
                        Id = attr.Id,
                        ParentId = attr.ParentId,
                        Name = attr.Name,
                        Order = attr.Order.ToInt(),
                        Url = $"/{controllerName}/{actionName}".ToLower(),
                        Icon = attr.Icon.IsNotBlank() ? attr.Icon : null
                    };
                    if (menus.Any(x => x.Id == menu.Id))
                    {
                        throw new BusinessException($"已经存在相同的Id={menu.Id},Name={menu.Name}");
                    }
                    menus.Add(menu);
                }
            }
            return menus;
        }
    }
}