
using System;
using System.Collections.Generic;
using ThreePoint.Core.Enums;
using ThreePoint.Core.ServerModels;

namespace ThreePoint.Core.Enities
{
    public class MenuEntity : BaseEntity
    {
        /// <summary>
        /// 上级ID
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 路径码=(上级的路径码+当前的Code)
        /// </summary>
        public string PathCode { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// URL
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 排序越大越靠后
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 菜单类型
        /// </summary>
        public MenuType Type { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// 角色菜单关系
        /// </summary>
        public virtual IList<RoleMenuEntity> RoleMenus { get; set; } = new List<RoleMenuEntity>();

        public static implicit operator MenuEntity(MenuDto v)
        {
            MenuEntity menu = new MenuEntity()
            {
                Id = v.Id,
                ParentId = v.ParentId,
                Name = v.Name,
                Url = v.Url,
                Order = v.Order,
                Icon = v.Icon,
                Type = v.Type,
            };
            return menu;
        }
    }

}