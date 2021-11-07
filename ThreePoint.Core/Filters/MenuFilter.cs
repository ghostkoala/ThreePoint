using ThreePoint.Core.Enums;

namespace ThreePoint.Core.Filters
{
    /// <summary>
    /// 菜单搜索过滤器
    /// </summary>
    public class MenuFilter : BaseFilter
    {
        /// <summary>
        /// 排除的类型
        /// </summary>
        public MenuType? ExcludeType { get; set; }

        /// <summary>
        /// 要查询的类型
        /// </summary>
        public MenuType? IncludeType { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool? IsEnable { get; set; }

        /// <summary>
        /// 指定搜索的模块Id
        /// </summary>
        public string SearchModuleId { get; set; }

        /// <summary>
        /// 指定搜索的菜单Id
        /// </summary>
        public string SearchMenuId { get; set; }
    }
}