using System;
using System.Collections.Generic;
using ThreePoint.Core.ServerModels;

namespace ThreePoint.Core.Enities
{
    /// <summary>
    /// 管理员实体
    /// </summary>
    public class AdminEntity : BaseEntity
    {
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
        /// 是否启用
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 部门Id
        /// </summary>
        public string DepartmentId { get; set; }

        /// <summary>
        /// 角色Id
        /// </summary>
        public string RoleId { get; set; }

        /// <summary>
        /// 最后修改用户
        /// </summary>
        public string UpdataUserId { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime UpdataTime { get; set; }

        /// <summary>
        /// 用户拥有的角色
        /// </summary>
        public virtual RoleEntity Role { get; set; }

        /// <summary>
        /// 部门主体
        /// </summary>
        public virtual DepartmentEntity Department { get; set; }

        /// <summary>
        /// 消息主体
        /// </summary>
        /// <value></value>
        public virtual IList<MessageReceiverEntity> MessageReceivers { get; set; } = new List<MessageReceiverEntity>();
    }
}