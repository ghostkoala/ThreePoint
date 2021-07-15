using System.ComponentModel.DataAnnotations;

namespace Logicore.Core.ViewModel
{
    /// <summary>
    /// 查看站内信视图
    /// </summary>
    public class ReceiverMessageViewModel
    {
        /// <summary>
        /// 标题
        /// </summary>
        /// <value></value>
        [Display(Name = "标题")]
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        /// <value></value>
        [Display(Name = "内容")]
        public string Contents { get; set; }

        /// <summary>
        /// 是否已读
        /// </summary>
        /// <value></value>
        [Display(Name = "是否已读")]
        public bool IsReaded { get; set; }

        /// <summary>
        /// 首次阅读时间
        /// </summary>
        /// <value></value>
        [Display(Name = "首次阅读时间")]
        public string FirstReadDate { get; set; }

        /// <summary>
        /// 下发时间
        /// </summary>
        /// <value></value>
        [Display(Name = "下发时间")]
        public string CreateDateTime { get; set; }
    }
}