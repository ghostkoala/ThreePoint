using Logicore.Core.Enums;
using Logicore.Core.Extensions;

namespace Logicore.Core.ViewModel
{
    /// <summary>
    /// 菜单视图
    /// </summary>
    public class MenuTableViewModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 菜单名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Url
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int order { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 菜单类型
        /// </summary>
        public MenuType Type { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string TypeName
        {
            get
            {
                return Type.GetDescriptionForEnum();
            }
        }

        /// <summary>
        /// 父级菜单名
        /// </summary>
        public string ParentName { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enable { get; set; }
    }
}