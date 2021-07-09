using System.ComponentModel.DataAnnotations;
using Logicore.Core.SystemConfigurationData;

namespace Logicore.Core.ServerModels
{
    /// <summary>
    /// 管理员数据
    /// </summary>
    public class AdminDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 登陆帐号
        /// </summary>
        [Display(Name = "登陆帐号")]
        [Required(ErrorMessage = ModelStateValidMessage.Required)]
        [MinLength(2, ErrorMessage = ModelStateValidMessage.MinLength)]
        [MaxLength(20, ErrorMessage = ModelStateValidMessage.MaxLength)]
        public string LoginName { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        [Display(Name = "真实姓名")]
        [Required(ErrorMessage = ModelStateValidMessage.Required)]
        [MinLength(2, ErrorMessage = ModelStateValidMessage.MinLength)]
        [MaxLength(20, ErrorMessage = ModelStateValidMessage.MaxLength)]
        public string RealName { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [Display(Name = "邮箱地址")]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Display(Name = "是否启用")]
        public bool Enabled { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        [Display(Name = "登陆密码")]
        [MinLength(4, ErrorMessage = ModelStateValidMessage.MinLength)]
        public string Password { get; set; }

        /// <summary>
        /// 部门Id
        /// </summary>
        [Display(Name = "部门Id")]
        public string DepartmentId { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        [Display(Name = "部门名称")]
        public string DepartmentName { get; set; }

        /// <summary>
        /// 角色Id
        /// </summary>
        [Display(Name = "角色Id")]
        public string RoleId { get; set; }

        /// <summary>
        /// 角色名
        /// </summary>
        [Display(Name = "角色名")]
        public string RoleName { get; set; }
    }
}