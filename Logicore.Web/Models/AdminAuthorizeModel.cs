using System;
using Logicore.Core.Enities;
using Logicore.Core.ServerModels;

namespace Logicore.Web.Models
{
    /// <summary>
    /// 用户验证模型
    /// </summary>
    public class AdminAuthorizeModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string roleId { get; set; }

        public static implicit operator AdminAuthorizeModel(AdminDto v)
        {
            var result = new AdminAuthorizeModel()
            {
                Id = v.Id,
                Name = v.LoginName,
                roleId = v.RoleId
            };
            return result;
        }
    }
}