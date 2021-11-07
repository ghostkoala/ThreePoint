using ThreePoint.Core.Enities.ServiceModel;

namespace ThreePoint.Core.Filters
{
    /// <summary>
    /// 搜索模式过滤
    /// </summary>
    public class DropDownDepartmentFilter
    {
        /// <summary>
        /// 搜索模式
        /// </summary>
        public DropDownOrSelectModel? model { get; set; }
    }
}