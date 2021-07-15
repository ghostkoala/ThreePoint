using System;

namespace Logicore.Core.ViewModel
{
    /// <summary>
    /// 站内日志数据
    /// </summary>
    public class MessageDetailViewModel
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 是否已读
        /// </summary>
        public bool IsReaded { get; set; }

        /// <summary>
        /// 查收日期
        /// </summary>
        public DateTime? ReadDate { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        /// <value></value>
        public DateTime CreateDateTime { get; set; }


    }
}