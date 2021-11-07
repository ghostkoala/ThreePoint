using ThreePoint.Core.Enities.ServiceModel;

namespace ThreePoint.Core.Filters
{
    /// <summary>
    /// 下拉菜单用户过滤
    /// </summary>
    public class DropDownAdminFilter
    {
        /// <summary>
        /// 搜索模式
        /// </summary>
        public DropDownOrSelectModel? model { get; set; }
    }
}