namespace ThreePoint.Core.Filters
{
    /// <summary>
    /// 部门搜索过滤
    /// </summary>
    public class DepartmentFilter : BaseFilter
    {
        /// <summary>
        /// 搜索名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 父级Id
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool? Enabled { get; set; }
    }
}