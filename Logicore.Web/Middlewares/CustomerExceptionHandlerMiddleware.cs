using System;
using System.Text.Json;
using System.Text.Unicode;
using Logicore.Core.Enities.ServiceModel;
using Logicore.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;

namespace Logicore.Web.Middlewares
{
    /// <summary>
    /// 自定义异常中间件
    /// </summary>
    public class CustomerExceptionMiddleware : IExceptionFilter, IFilterMetadata
    {
        private string Id = Guid.NewGuid().ToString();
        private readonly ILogger<CustomerExceptionMiddleware> _logger;

        private JsonSerializerOptions _JsonSerializerOptions = new JsonSerializerOptions()
        {
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(UnicodeRanges.All)
        };

        public CustomerExceptionMiddleware(ILogger<CustomerExceptionMiddleware> logger
        , IModelMetadataProvider modelMetadataProvider)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            ResultModel<bool> data;
            string message = string.Empty;
            int code = 500;
            var e = context.Exception;
            if (e is BusinessException businessException)
            {
                message = e.Message;
                code = businessException.Code;
                _logger.Log(LogLevel.Warning, String.Format("Request error on url {0}: {1}", context.HttpContext.Request.Path, message), context.Exception);
            }
            else if (e is ServerException serverException)
            {
                message = e.Message;
                code = serverException.Code;
                _logger.Log(LogLevel.Error, String.Format("Request error on url {0}: {1}", context.HttpContext.Request.Path, message), context.Exception);
            }
            else
            {
                message = e.Message;
                code = 500;
            }

            data = new ResultModel<bool>(code, message, false);

            if (context.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                // context.HttpContext.Response.ContentType = "application/json;charset=utf-8";
                // var json = System.Text.Json.JsonSerializer.Serialize(data, _JsonSerializerOptions);
                // await context.HttpContext.Response.WriteAsync(json);                
                JsonResult jsonResult = new JsonResult(data);
                context.Result = jsonResult;
            }
            else
            {
                context.Result = new RedirectResult("~/Home/Error");//跳转至错误提示页面
            }
            context.ExceptionHandled = false;
        }
    }
}