namespace Logicore.Core.Filters
{
    /// <summary>
    /// 站内信过滤
    /// </summary>
    public class MessageFilter : BaseFilter
    {
        /// <summary>
        /// 搜索标题
        /// </summary>
        /// <value></value>
        public string SearchTitle { get; set; }

        /// <summary>
        /// 搜索用户Id
        /// </summary>
        /// <value></value>
        public string SearchUserId { get; set; }
    }
}