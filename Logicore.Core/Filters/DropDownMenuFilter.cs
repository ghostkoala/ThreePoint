using Logicore.Core.Enities.ServiceModel;
using Logicore.Core.Enums;

namespace Logicore.Core.Filters
{
    /// <summary>
    /// 菜单下拉框搜索过滤
    /// </summary>
    public class DropDownMenuFilter
    {
        /// <summary>
        /// 菜单类型
        /// </summary>
        public MenuType? type { get; set; }

        /// <summary>
        /// 搜索模式
        /// </summary>
        public DropDownOrSelectModel? model { get; set; }

        /// <summary>
        /// 指定搜索父级菜单Id
        /// </summary>
        public string menuId { get; set; }
    }
}