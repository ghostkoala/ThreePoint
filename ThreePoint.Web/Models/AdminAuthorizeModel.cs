using System;
using ThreePoint.Core.Enities;
using ThreePoint.Core.ServerModels;

namespace ThreePoint.Web.Models
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