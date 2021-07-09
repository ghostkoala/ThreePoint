namespace Logicore.Core.Filters
{
    /// <summary>
    /// 管理员搜索过滤
    /// </summary>
    public class AdminFilter : BaseFilter
    {
        /// <summary>
        /// 登陆帐号
        /// </summary>
        public string SearchLoginName { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string SearchRealName { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool? IsEnabled { get; set; }

        /// <summary>
        /// 部门Id
        /// </summary>
        public string SearchDepartmentId { get; set; }

        /// <summary>
        /// 角色Id
        /// </summary>
        public string SearchRoleId { get; set; }
    }
}