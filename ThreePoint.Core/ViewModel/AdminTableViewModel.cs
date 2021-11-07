using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ThreePoint.Core.Enities;
using ThreePoint.Core.ServerModels;

namespace ThreePoint.Core.ViewModel
{
    /// <summary>
    /// 管理员视图
    /// </summary>
    public class AdminTableViewModel
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 登录账号
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "创建时间")]
        public string CreateDateTime { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enable { get; set; }

        /// <summary>
        /// 角色Id
        /// </summary>
        public string RoleId { get; set; }

        /// <summary>
        /// 角色名
        /// </summary>
        public string RoleName { get; set; }
    }
}