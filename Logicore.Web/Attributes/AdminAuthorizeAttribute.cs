using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Filters;
using Logicore.Core.Exceptions;
using Microsoft.Extensions.Logging;
using Logicore.Web.Servers;
using Microsoft.Extensions.Configuration;
using Logicore.Core.Extensions;
using Logicore.Web.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

//[ServiceFilter(typeof(AdminAuthorizeAttribute))]
namespace Logicore.Web.Attributes
{
    public class AdminAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly IServiceFactory _serviceFactory;
        private readonly ILogger<AdminAuthorizeAttribute> _logger;
        private readonly IConfiguration _configuration;

        public AdminAuthorizeAttribute(IServiceFactory serviceFactory,
        ILogger<AdminAuthorizeAttribute> logger,
        IConfiguration configuration)
        {
            _serviceFactory = serviceFactory;
            _logger = logger;
            _configuration = configuration;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //匿名标识 无需验证
            if (context.Filters.Any(e => (e as AllowAnonymous) != null))
                return;

            //此处获取的登录用户信息
            var AdminAuth = _serviceFactory.GetLoginInfoAsync().Result;
            //未登陆就抛出异常
            if (AdminAuth == null)
            {
                //throw new BusinessException("用户未登陆", 401);
                context.Result = new RedirectToActionResult("Login", "Home", null);
                return;
            }
            else
            {
                //是管理员就获取所有权限
                var goldList = _configuration.GetValue<string>("GoldList", "").Split(',');
                if (goldList.Any(x => x == AdminAuth.Name))
                    return;

                //对应action方法或者Controller上若存在NonePermissionAttribute标识，即表示为管理员的默认权限,只要登录就有权限
                var isNone = context.Filters.Any(e => (e as NonePermissionAttribute) != null);
                if (isNone)
                    return;

                //获取请求的区域，控制器，action名称
                var area = context.RouteData.DataTokens["area"]?.ToString().ToLower();
                var controller = context.RouteData.Values["controller"]?.ToString().ToLower();
                var action = context.RouteData.Values["action"]?.ToString().ToLower();
                string url = "";
                if (area.IsNotBlank()) url = "/" + area;
                url = url + "/" + controller + "/" + action;
                var isPermit = false;

                //校验权限
                //isPermit = true; 
                isPermit = _serviceFactory.CheckPermitAsync(AdminAuth.roleId, url).Result;
                if (isPermit)
                    return;
                //此action方法的父辈权限判断，只要有此action对应的父辈权限，皆有权限访问
                var pAttrs = context.Filters.Where(e => (e as ParentPermissionAttribute) != null).ToList();
                if (pAttrs.Count > 0)
                {
                    foreach (ParentPermissionAttribute pattr in pAttrs)
                    {
                        url = "";
                        if (pattr.Area.IsNotBlank()) url = "/" + area;
                        url = url + "/" + pattr.Controller + "/" + pattr.Action;
                        isPermit = _serviceFactory.CheckPermitAsync(AdminAuth.roleId, url).Result;
                        if (isPermit)
                            return;
                    }
                }
            }

            //全无限权将抛出错误
            //throw new BusinessException("无访问限权", 403);
            context.Result = new JsonResult("无此限权");
        }
    }
}