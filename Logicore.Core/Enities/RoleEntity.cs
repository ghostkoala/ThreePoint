using System;
using System.Collections.Generic;
using Logicore.Core.ServerModels;

namespace Logicore.Core.Enities
{
    /// <summary>
    /// 角色实体
    /// </summary>
    public class RoleEntity : BaseEntity
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// 角色菜单关系
        /// </summary>
        public virtual IList<RoleMenuEntity> RoleMenus { get; set; } = new List<RoleMenuEntity>();

        /// <summary>
        /// 此角色下的管理员
        /// </summary>
        public virtual IList<AdminEntity> Admins { get; set; } = new List<AdminEntity>();
    }
}