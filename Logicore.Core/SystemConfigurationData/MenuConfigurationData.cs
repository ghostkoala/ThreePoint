namespace Logicore.Core.SystemConfigurationData
{

    /// <summary>
    /// 菜单数据配置
    /// </summary>
    public class Menu
    {
        #region 模块参数配置

        /// <summary>
        /// 系统设置模块
        /// </summary>
        public static (string Id, string Name) System = (SystemId, "系统设置");

        /// <summary>
        /// 日志查看模块
        /// </summary>
        public static (string Id, string Name) Logs = (LogsId, "日志查看");

        /// <summary>
        /// 示例页面模块
        /// </summary>
        public static (string Id, string Name) Pages = (PagesId, "示例页面");

        #endregion

        #region 菜单常量配置

        /// <summary>
        /// 系统设置模块Id
        /// </summary>
        public const string SystemId = "00001";

        /// <summary>
        /// 日志查看模块Id
        /// </summary>
        public const string LogsId = "00002";

        /// <summary>
        /// 示例页面模块Id
        /// </summary>
        public const string PagesId = "00003";

        /// <summary>
        /// 菜单管理页面Id
        /// </summary>
        public const string MenuPageId = "00015";

        /// <summary>
        /// 菜单管理添加Id
        /// </summary>
        public const string MenuAddId = "00016";

        /// <summary>
        /// 菜单管理编辑Id
        /// </summary>
        public const string MenuEditId = "00017";

        /// <summary>
        /// 菜单管理删除Id
        /// </summary>
        public const string MenuDeleteId = "00018";

        /// <summary>
        /// 角色管理页面Id
        /// </summary>
        public const string RolePageId = "00019";

        /// <summary>
        /// 角色管理添加Id
        /// </summary>
        public const string RoleAddId = "00020";

        /// <summary>
        /// 角色管理编辑Id
        /// </summary>
        public const string RoleEditId = "00021";

        /// <summary>
        /// 角色管理删除Id
        /// </summary>
        public const string RoleDeleteId = "00022";

        /// <summary>
        /// 角色授权页面Id
        /// </summary>
        public const string RoleAuthorizeId = "00023";

        /// <summary>
        /// 设置角色权限Id
        /// </summary>
        public const string RoleSetAuthorizeId = "00024";

        /// <summary>
        /// 取消角色授权Id
        /// </summary>
        public const string RoleCancelAuthorizeId = "00025";

        /// <summary>
        ///　修改角色限权
        /// </summary>
        public const string EditPermissionWithRoleId = "00026";

        /// <summary>
        /// 管理员管理页Id
        /// </summary>
        public const string AdminPageId = "00030";

        /// <summary>
        /// 管理员密码修改
        /// </summary>
        public const string AdminEditPassWordId = "00031";

        /// <summary>
        /// 管理员基本信息修改
        /// </summary>
        public const string AdminEditGeneralInfoId = "00032";

        /// <summary>
        /// 删除管理员
        /// </summary>
        public const string AdminDeleteId = "00034";

        /// <summary>
        /// 修改管理员信息
        /// </summary>
        public const string AdminEditId = "00035";

        /// <summary>
        /// 增加管理员信息
        /// </summary>
        public const string AdminAddId = "00036";

        /// <summary>
        /// 部门管理页
        /// </summary>
        public const string DepartmentPageId = "00040";

        /// <summary>
        /// 添加部门
        /// </summary>
        public const string DepartmentAddId = "00041";

        /// <summary>
        /// 修改部门
        /// </summary>
        public const string DepartmentEditId = "00042";

        /// <summary>
        /// 删除部门
        /// </summary>
        public const string DepartmentDeleteId = "00043";

        /// <summary>
        /// 站内信管理页
        /// </summary>
        public const string MessagePageId = "00050";

        /// <summary>
        /// 站内信管理页
        /// </summary>
        public const string MessageSendId = "00051";

        /// <summary>
        /// 站内信管理页
        /// </summary>
        public const string MessageEditId = "00052";

        /// <summary>
        /// 站内信管理页
        /// </summary>
        public const string MessageDeleteId = "00053";

        /// <summary>
        /// 系统错误日志查询
        /// </summary>
        public const string ServerExceptionPageId = "00060";

        /// <summary>
        /// 系统信息配置
        /// </summary>
        public const string ServerConfigPageId = "00061";

        /// <summary>
        /// 系统信息配置
        /// </summary>
        public const string ServerConfigEditId = "00062";
        #endregion



        #region 任务模板常量配置

        /// <summary>
        /// 任务模板首页页面Id
        /// </summary>
        public const string TaskTemplatePageId = "10000";

        /// <summary>
        /// 任务模板添加页面Id
        /// </summary>
        public const string TaskTemplateAddId = "10001";

        #endregion

    }
}