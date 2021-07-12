using System.Collections.Generic;

namespace Logicore.Core.Enities
{
    /// <summary>
    /// 站内信
    /// </summary>
    public partial class MessageEntity : BaseEntity
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Contents { get; set; }

        /// <summary>
        /// 已读人数
        /// </summary>
        public int ReadedNumber { get; set; } = 0;

        /// <summary>
        /// 总接收人数
        /// </summary>
        public int Total { get; set; } = 0;

        /// <summary>
        /// 消息接收人
        /// </summary>
        public virtual IList<MessageReceiverEntity> MessageReceivers { get; set; } = new List<MessageReceiverEntity>();
    }
}