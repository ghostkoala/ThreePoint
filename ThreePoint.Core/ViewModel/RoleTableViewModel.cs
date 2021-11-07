using System;
using ThreePoint.Core.Enities;

namespace ThreePoint.Core.ViewModel
{
    /// <summary>
    /// 角色视图
    /// </summary>
    public class RoleTableViewModel
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 角色名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string CreateUser { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateDateTime { get; set; }
    }
}