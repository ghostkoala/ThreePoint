using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Logicore.Core.SystemConfigurationData;

namespace Logicore.Core.ServerModels
{
    /// <summary>
    /// 站点信数据
    /// </summary>
    public class MessageDto
    {
        /// <summary>
        /// id
        /// </summary>
        /// <value></value>
        [Display(Name = "Id")]
        public string Id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        /// <value></value>
        [Display(Name = "标题")]
        [Required(ErrorMessage = ModelStateValidMessage.Required)]
        [MinLength(2, ErrorMessage = ModelStateValidMessage.MinLength)]
        [MaxLength(40, ErrorMessage = ModelStateValidMessage.MaxLength)]
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        /// <value></value>
        [Display(Name = "内容")]
        [Required(ErrorMessage = ModelStateValidMessage.Required)]
        [MinLength(2, ErrorMessage = ModelStateValidMessage.MinLength)]
        [MaxLength(500, ErrorMessage = ModelStateValidMessage.MaxLength)]
        public string Contents { get; set; }

        /// <summary>
        /// 是否发送至所有人
        /// </summary>
        /// <value></value>
        [Display(Name = "发送选项")]
        [Required(ErrorMessage = ModelStateValidMessage.Required)]
        public SendModel SendModel { get; set; }

        /// <summary>
        /// 接收者
        /// </summary>
        /// <value></value>
        [Display(Name = "接收者")]
        public IEnumerable<string> ReceiverIds { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum SendModel
    {
        //所有人
        Toall,
        //个人
        person
    }
}