using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ThreePoint.Core.Enities;

namespace ThreePoint.Core.ViewModel
{
    /// <summary>
    /// 登录视图
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// 登录名
        /// </summary>
        [Display(Name = "登录账号"), Required(ErrorMessage = "请输入用户帐号"), MinLength(4, ErrorMessage = "帐号输入错误"), MaxLength(20, ErrorMessage = "帐号输入错误")]
        [RegularExpression("^[^_][a-zA-Z0-9_]*$", ErrorMessage = "登录账号必须是字母、数字或者下划线的组合")]
        public string LoginName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Display(Name = "登录密码")]
        [Required(ErrorMessage = "请输入密码"), MinLength(6, ErrorMessage = "密码不小于6位"), MaxLength(12, ErrorMessage = "密码不大于12位")]
        public string Password { get; set; }

        /// <summary>
        /// 记住我
        /// </summary>
        public bool RememberMe { get; set; }

        /// <summary>
        /// 登陆成功后跳转的地址
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        /// 登录IP
        /// </summary>
        public string LoginIP { get; set; }
    }
}