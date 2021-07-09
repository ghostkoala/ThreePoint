using System.Collections.Generic;

namespace Logicore.Core.Enities
{
    /// <summary>
    /// 部门实体
    /// </summary>
    public class DepartmentEntity : BaseEntity
    {
        public DepartmentEntity()
        {
            Admins = new List<AdminEntity>();
        }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 全称(上级部门的名称-当前部门名称)
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// 上级部门
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 路径码=(上级的路径码+当前的Code)
        /// </summary>
        public string PathCode { get; set; }

        /// <summary>
        /// 是否户用
        /// </summary>
        public bool Enable { get; set; } = true;

        /// <summary>
        /// 此部门下的管理员
        /// </summary>
        public virtual IList<AdminEntity> Admins { get; set; }
    }
}