using System;
using System.Collections.Generic;

namespace Logicore.Core.Enities
{
    /// <summary>
    /// 消息接收人
    /// </summary>
    public partial class MessageReceiverEntity : BaseEntity
    {
        /// <summary>
        /// 消息Id
        /// </summary>
        public string MessageId { get; set; }

        /// <summary>
        /// 所属用户Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 是否已读
        /// </summary>
        public bool IsReaded { get; set; } = false;

        /// <summary>
        /// 查看时间
        /// </summary>
        public DateTime? ReadDate { get; set; }

        /// <summary>
        /// 管理员主体
        /// </summary>
        /// <value></value>
        public virtual AdminEntity Admin { get; set; } = new AdminEntity();

        /// <summary>
        /// 消息主体
        /// </summary>
        public virtual MessageEntity Message { get; set; } = new MessageEntity();
    }
}