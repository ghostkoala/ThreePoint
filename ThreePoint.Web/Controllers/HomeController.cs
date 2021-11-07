using System;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;
using ThreePoint.Core.ViewModel;
using ThreePoint.IServices;
using ThreePoint.Web.Attributes;
using ThreePoint.Web.Filters;
using ThreePoint.Web.Models;
using ThreePoint.Core.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using ThreePoint.Core.Exceptions;

namespace ThreePoint.Web.Controllers
{
    /// <summary>
    /// 首页
    /// </summary>
    //[ServiceFilter(typeof(AdminAuthorizeAttribute))]
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IDistributedCache _cache;
        private readonly IAdminService _adminService;
        private readonly IMenuService _menuService;
        private readonly IMessageService _messageService;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// ctor
        /// </summary>
        public HomeController(ILogger<HomeController> logger,
         IHttpContextAccessor httpContext,
         IDistributedCache cache,
         IAdminService adminService,
         IMenuService menuService,
         IMessageService messageService,
         IConfiguration configuration
         )
        {
            _logger = logger;
            _httpContext = httpContext;
            _cache = cache;
            _adminService = adminService;
            _menuService = menuService;
            _messageService = messageService;
            _configuration = configuration;
        }

        /// <summary>
        /// 首页
        /// </summary>
        [NonePermission]
        public async Task<IActionResult> Index()
        {
            if (!CheckLoginStatus()) return View("Login");
            ViewData["title"] = "Logicore - ESM管理系统首页";
            var userId = HttpContext.Session.GetString("Uid");
            var admin = await _adminService.FindAsync(userId);
            var goldList = _configuration.GetValue<string>("GoldList", "").Split(',');
            var isSupper = goldList.Any(x => x == admin.LoginName);
            var myMenus = await _menuService.GetMyMenusAsync(userId, isSupper);
            var myUnReadMessageNumber = await _messageService.GetMyMessageCountAsync(userId);
            var myUnReadMessages = await _messageService.GetUnReadMesasgeAsync(userId);
            ViewBag.Menus = myMenus;
            ViewBag.MyUnReadMessageNumber = myUnReadMessageNumber;
            return View(myUnReadMessages);
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            if (CheckLoginStatus()) return View("Index");
            else return View();
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (CheckLoginStatus()) return View("Index");
            if (!ModelState.IsValid) return View(model);
            var connection = Request.HttpContext.Features.Get<IHttpConnectionFeature>();
            //var localIpAddress = connection.LocalIpAddress;    //本地IP地址
            //var localPort = connection.LocalPort;              //本地IP端口
            var remoteIpAddress = connection.RemoteIpAddress;  //远程IP地址
            //var remotePort = connection.RemotePort;            //本地IP端口

            model.LoginIP = remoteIpAddress.ToString();
            var result = await _adminService.LoginAsync(model);
            if (result.Code == 200)
            {
                this.HttpContext.Session.SetString("Uid", result.Data.Id);
                var authenType = CookieAuthenticationDefaults.AuthenticationScheme;
                var identity = new ClaimsIdentity(authenType);
                identity.AddClaim(new Claim(ClaimTypes.Name, result.Data.LoginName));
                identity.AddClaim(new Claim("LoginUserId", result.Data.Id.ToString()));
                var properties = new AuthenticationProperties() { IsPersistent = true };
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(authenType, principal, properties);
                model.ReturnUrl = model.ReturnUrl.IsNotBlank() ? model.ReturnUrl : "/";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("LoginName", result.Message);
            return View(model);
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        /// <summary>
        /// 初始化数据库
        /// </summary>
        [Obsolete]
        public IActionResult InitAsync()
        {
            //var menues = MenuHelper.GetMenues();
            //await databaseInit.InitAsync(menues);
            return Ok();
        }

        /// <summary>
        /// 错误页面
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [AllowAnonymous]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [AllowAnonymous]
        public IActionResult WelCome()
        {
            return View();
        }

        /// <summary>
        /// 检查登陆状态
        /// </summary>
        /// <returns></returns>
        private bool CheckLoginStatus()
        {
            if (this.HttpContext.Session.GetString("Uid") != null) return true;
            else return false;
        }

        [AllowAnonymous]
        [Obsolete]
        public IActionResult ThrowErr() => throw new BusinessException("this is test err");
    }
}
