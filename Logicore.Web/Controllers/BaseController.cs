using Logicore.Web.Attributes;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Logicore.Web.Controllers
{
    /// <summary>
    /// 基础控制器
    /// </summary>
    [ServiceFilter(typeof(AdminAuthorizeAttribute), IsReusable = false)]  //统一权限过滤
    [EnableCors("Any")]  //统一设置Cors策略
    public class BaseController : Controller
    {
        /*
        * 对控制器附加的操作，或控制器必要变量定义都在这里
        */
    }
}