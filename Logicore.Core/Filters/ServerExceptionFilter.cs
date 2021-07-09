using System;
using Logicore.Core.Enities;

namespace Logicore.Core.Filters
{
    /// <summary>
    /// 系统错误信息过滤
    /// </summary>
    public class ServerExceptionFilter : BaseFilter
    {
        /// <summary>
        /// 开始查找时间
        /// </summary>
        public DateTime? StartTime;

        /// <summary>
        /// 结束查找时间
        /// </summary>
        public DateTime? EndTime;

        /// <summary>
        /// 错误类型
        /// </summary>
        public ErrCategory? category;
    }
}