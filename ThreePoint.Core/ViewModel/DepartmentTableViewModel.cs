namespace ThreePoint.Core.ViewModel
{
    /// <summary>
    /// 部门视图
    /// </summary>
    public class DepartmentTableViewModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 名称
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
        /// 是否启用
        /// </summary>
        public bool Enable { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }
    }
}