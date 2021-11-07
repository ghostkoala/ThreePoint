using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ThreePoint.Web.Extensions
{
    public class RedirectHelper
    {
        public static AjaxModel AjaxModel(int code, string msg, dynamic data = null, string url = null)
        {
            return new AjaxModel()
            {
                Code = code,
                Msg = msg,
                Data = data,
                Url = url
            };
        }
        public static ActionResult JsonError()
        {
            JsonResult json = new JsonResult(AjaxModel(500, "系统出错啦"));
            return json;
        }

        public static ActionResult UrlFail(RouteData route)
        {
            RedirectToActionResult result = new RedirectToActionResult(route.Values["action"].ToString(), route.Values["controller"].ToString(), null);
            return result;
        }
    }

    /// <summary>
    /// Json返回结构
    /// </summary>
    public class AjaxModel
    {
        public int Code { get; set; }
        public string Msg { get; set; }
        public object Data { get; set; }
        public string Url { get; set; }
    }
}