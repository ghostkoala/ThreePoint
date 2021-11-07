namespace ThreePoint.Core.Filters
{
    /// <summary>
    /// 角色搜索过滤
    /// </summary>
    public class RoleFilter : BaseFilter
    {
        /// <summary>
        /// 搜索名
        /// </summary>
        public string SearchName { get; set; }
        /// <summary>
        /// 搜索描述
        /// </summary>
        public string SearchDescription { get; set; }
    }
}