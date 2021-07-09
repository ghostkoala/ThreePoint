using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Logicore.Core.Enities;
using Logicore.Core.SystemConfigurationData;

namespace Logicore.Core.ServerModels
{
    /// <summary>
    /// 管理员基本信息数据
    /// </summary>
    public class AdminGeneralInfoDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        [Display(Name = "真实姓名")]
        [Required(ErrorMessage = ModelStateValidMessage.Required)]
        public string RealName { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [Display(Name = "电子邮箱")]
        [EmailAddress(ErrorMessage = "请输入正确的邮箱")]
        public string Email { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }
}