using System;
using System.Collections.Generic;

namespace Logicore.Core.ViewModel
{
    /// <summary>
    /// 站内信息数据
    /// </summary>
    public class MessageQueryViewModel
    {
        /// <summary>
        /// ctor
        /// </summary>
        public MessageQueryViewModel()
        {
            Details = new List<MessageDetailViewModel>();
        }
        public string Id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Contents { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateDateTime { get; set; }

        /// <summary>
        /// 已读人数
        /// </summary>
        public int ReadedNumber { get; set; }

        /// <summary>
        /// 总接收人数
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// 详情
        /// </summary>
        public IList<MessageDetailViewModel> Details { get; set; }
    }
}