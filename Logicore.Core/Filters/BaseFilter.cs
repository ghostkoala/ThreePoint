namespace Logicore.Core.Filters
{
    /// <summary>
    /// 基本过滤器
    /// </summary>
    public class BaseFilter
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        public int Offset { get; set; } = 1;

        /// <summary>
        /// 每页显示的数据量
        /// </summary>
        public int Limit { get; set; } = 10;

        /// <summary>
        /// 搜索关键字
        /// </summary>
        public string Keywords { get; set; } = "";

        /// <summary>
        /// 排序 "asc" or "desc"
        /// </summary>
        public string Order { get; set; } = "desc";
    }
}