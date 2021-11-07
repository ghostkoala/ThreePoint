using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ThreePoint.Core.SystemConfigurationData;

namespace ThreePoint.Core.ServerModels
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
        [Display(Name = "发送给所有人")]
        public bool IsToAll { get; set; }

        /// <summary>
        /// 接收者
        /// </summary>
        /// <value></value>
        [Display(Name = "接收者")]
        public IEnumerable<string> ReceiverIds { get; set; }
    }

}