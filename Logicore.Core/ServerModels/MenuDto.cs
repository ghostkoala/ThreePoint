using System;
using System.ComponentModel.DataAnnotations;
using Logicore.Core.Enities;
using Logicore.Core.Enums;
using Logicore.Core.Extensions;

namespace Logicore.Core.ServerModels
{
    /// <summary>
    /// 菜单数据
    /// </summary>
    public class MenuDto
    {
        /// <summary>
        /// 菜单ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 上级ID
        /// </summary>
        [Display(Name = "上级菜单")]
        public string ParentId { get; set; }

        /// <summary>
        /// 上级菜单名称
        /// </summary>
        [Display(Name = "上级菜单")]
        public string ParentName { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = "菜单名称"), Required(ErrorMessage = "菜单名称必填"),
            MinLength(2, ErrorMessage = "菜单名称最少输入2个字符"),
            MaxLength(20, ErrorMessage = "菜单名称最大输入20个字符")]
        public string Name { get; set; }

        /// <summary>
        /// URL
        /// </summary>
        [Display(Name = "菜单网址"), Required(ErrorMessage = "菜单网址必填"),
            MaxLength(300, ErrorMessage = "菜单网址最大l输入300个字符")]
        public string Url { get; set; }

        /// <summary>
        /// 排序越大越靠后
        /// </summary>
        [Display(Name = "排序数字")]
        public int Order { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        [Display(Name = "图标")]
        public string Icon { get; set; }

        /// <summary>
        /// 菜单类型
        /// </summary>
        [Display(Name = "类型")]
        public MenuType Type { get; set; }

        /// <summary>
        /// 菜单类型名称
        /// </summary>
        public string TypeName
        {
            get { return Type.GetDescriptionForEnum(); }
        }

        public static implicit operator MenuDto(MenuEntity v)
        {
            MenuDto menu = new MenuDto()
            {
                Id = v.Id,
                ParentId = v.Id,
                Name = v.Name,
                Url = v.Url,
                Order = v.Order,
                Icon = v.Icon,
                Type = v.Type
            };
            return menu;
        }
    }
}