using System;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using ThreePoint.Core.Enities;
using ThreePoint.Core.Exceptions;
using ThreePoint.IServices;
using ThreePoint.Web.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace ThreePoint.Web.Filters
{
    //全局异常处理
    public class GlobalExceptionFilter : IAsyncExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;
        private readonly IServerExceptionService _serverExceptionService;
        private JsonSerializerOptions _JsonSerializerOptions = new JsonSerializerOptions()
        {
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(UnicodeRanges.All)
        };

        /// <summary>
        /// ctor
        /// </summary>
        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger, IServerExceptionService serverExceptionService)
        {
            _logger = logger;
            _serverExceptionService = serverExceptionService;
        }

        public Task OnExceptionAsync(ExceptionContext context)
        {
            string message;
            int code = 500;
            ServerExceptionEntity entity = new ServerExceptionEntity();
            if (context.ExceptionHandled == false)
            {
                var e = context.Exception;
                if (e is BusinessException businessException)
                {
                    message = e.Message;
                    code = businessException.Code;
                    entity.errCategory = ErrCategory.BusinessException;
                    _logger.Log(LogLevel.Warning, String.Format("Request error on url {0}: {1}", context.HttpContext.Request.Path, message), context.Exception);
                }
                else if (e is ServerException serverException)
                {
                    message = e.Message;
                    code = serverException.Code;
                    entity.errCategory = ErrCategory.ServerException;
                    _logger.Log(LogLevel.Warning, String.Format("Request error on url {0}: {1}", context.HttpContext.Request.Path, message), context.Exception);
                }
                else
                {
                    entity.errCategory = ErrCategory.Others;
                    message = e.Message;
                    _logger.Log(LogLevel.Warning, String.Format("Request error on url {0}: {1}", context.HttpContext.Request.Path, message), context.Exception);
                }
                entity.useId = context.HttpContext.Session.GetString("Uid") ?? "无登陆用户";
                entity.Code = code;
                entity.ErrMessage = message;
                entity.StackTrace = e.StackTrace;
                entity.Method = context.HttpContext.Request.Method;
                if (context.HttpContext.Request.Query != null)
                    entity.ActionArguments = context.HttpContext.Request.Query.ToString();
                else
                    entity.ActionArguments = context.HttpContext.Request.Form.ToString();
                entity.Url = context.HttpContext.Request.Host + context.HttpContext.Request.Path + context.HttpContext.Request.QueryString;
                _serverExceptionService.AddAsync(entity);


                //通过HTTP请求头来判断是否为Ajax请求，Ajax请求的request headers里都会有一个key为x-requested-with，值“XMLHttpRequest”
                var requestData = context.HttpContext.Request.Headers.ContainsKey("x-requested-with");
                bool IsAjax = false;
                if (requestData)
                {
                    IsAjax = context.HttpContext.Request.Headers["x-requested-with"] == "XMLHttpRequest" ? true : false;
                }

                //不是异步请求则跳转页面，异步请求则返回json
                if (!IsAjax)
                {
                    context.RouteData = new Microsoft.AspNetCore.Routing.RouteData();
                    context.RouteData.Values.Add("controller", "home");
                    context.RouteData.Values.Add("action", "error");
                    context.Result = RedirectHelper.UrlFail(context.RouteData);
                }
                else
                    context.Result = RedirectHelper.JsonError();
            }

            context.ExceptionHandled = true; //异常已处理了
            return Task.CompletedTask;
        }
    }
}