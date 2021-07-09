using System.ComponentModel.DataAnnotations;
using Logicore.Core.SystemConfigurationData;

namespace Logicore.Core.ServerModels
{
    /// <summary>
    /// 管理员密码修改视图
    /// </summary>
    public class AdminEditPassWordDto
    {
        /// <summary>
        /// 管理员Id
        /// </summary>
        [Display(Name = "管理员Id")]
        public string Id { get; set; }

        /// <summary>
        /// 旧密码
        /// </summary>
        [Display(Name = "旧密码")]
        [Required(ErrorMessage = ModelStateValidMessage.Required)]
        [MinLength(6, ErrorMessage = ModelStateValidMessage.MinLength)]
        public string OldPassWord { get; set; }

        /// <summary>
        /// 新密码
        /// </summary>
        [Display(Name = "新密码")]
        [Required(ErrorMessage = ModelStateValidMessage.Required)]
        [MinLength(6, ErrorMessage = ModelStateValidMessage.MinLength)]
        public string NewPassWord { get; set; }

        /// <summary>
        /// 确认密码
        /// </summary>
        [Display(Name = "确认密码")]
        public string ConfirmPassword { get; set; }
    }
}