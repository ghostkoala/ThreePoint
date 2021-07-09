using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Logicore.Web.Filters
{
    /// <summary>
    /// 默认权限
    /// </summary>
    public class NonePermissionAttribute : ActionFilterAttribute { }

    /// <summary>
    /// 匿名验证--不需要验证，允许所有人访问
    /// </summary>
    public class AllowAnonymous : ActionFilterAttribute { }

    /// <summary>
    /// 长辈权限
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class ParentPermissionAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 区域
        /// </summary>
        public string Area { get; set; }
        /// <summary>
        /// 控制器
        /// </summary>
        public string Controller { get; set; }
        /// <summary>
        /// Action名称
        /// </summary>
        public string Action { get; set; }

        public ParentPermissionAttribute(string area, string controller, string action)
        {
            this.Area = area;
            this.Controller = controller;
            this.Action = action;
        }

        public ParentPermissionAttribute(string controller, string action)
        {
            this.Controller = controller;
            this.Action = action;
        }
    }
}