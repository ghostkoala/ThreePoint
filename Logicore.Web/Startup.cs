using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.Cookies;
using Logicore.Web.Filters;
using Logicore.Web.Extensions;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Logicore.IRepository;
using Microsoft.Extensions.Logging;
using Logicore.Web.Middlewares;
using Logicore.Web.Attributes;

namespace Logicore.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region 启用数据注入
            services.AddDataService();  //依赖注入组件
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<AdminAuthorizeAttribute>();  //注册AuthorizeFilterAttribute为组件
            #endregion

            services.AddHttpContextAccessor();

            #region 启用Session服务
            //启用Session服务，启用redis，自动开始分布式session，无需另外配置
            var sessionOutTime = Configuration.GetValue<int>("WebConfig:SessionTimeOut", 30);
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(sessionOutTime);
                options.Cookie.HttpOnly = true;
            });
            #endregion

            #region 权限配置
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
            });
            //添加授权支持，并添加使用Cookie的方式，配置登录页面和没有权限时的跳转页面
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(o =>
                {
                    o.ExpireTimeSpan = TimeSpan.FromMinutes(480);//cookie默认有效时间为8个小时
                    o.LoginPath = new PathString("/Home/Login");
                    o.LogoutPath = new PathString("/Home/Logout");
                    o.Cookie = new CookieBuilder
                    {
                        HttpOnly = true,
                        Name = ".Logicore.Core.Identity",
                        Path = "/"
                    };
                    //o.DataProtectionProvider = null;//如果需要做负载均衡，就需要提供一个Key
                });
            #endregion

            #region session redis存储
            bool openRedis = Configuration.GetValue<bool>("WebConfig:Redis:Open", false);
            //Session 过期时长分钟
            if (openRedis == true)
            {
                //var redis = StackExchange.Redis.ConnectionMultiplexer.Connect(redisConn);
                //services.AddDataProtection().PersistKeysToRedis(redis, "DataProtection-Test-Keys");
                var redisConn = Configuration["WebConfig:Redis:Connection"];
                var redisInstanceName = Configuration["WebConfig:Redis:InstanceName"];
                services.AddDistributedRedisCache(option =>
                {
                    //redis 连接字符串
                    option.Configuration = redisConn;
                    //redis 实例名
                    option.InstanceName = redisInstanceName;
                }
                );
            }
            #endregion

            #region 跨站请求伪造XSRF/CSRF配置
            // services.AddAntiforgery(options =>
            // {
            //     // Set Cookie properties using CookieBuilder properties†.
            //     options.FormFieldName = "AntiforgeryKey_YiKong";
            //     options.HeaderName = "X-CSRF-TOKEN-yilezhu";
            //     options.SuppressXFrameOptionsHeader = false;
            // });
            #endregion

            #region 启用控制器到视图服务
            //注册控制器到视图服务，MVC服务过于强大，没必要全引用，此处单独引用部分功能
            services.AddControllersWithViews(option =>
            {
                //配置全局异常过滤
                option.Filters.Add<GlobalExceptionFilter>();
            }).AddControllersAsServices().AddJsonOptions(options =>
            {
                // options.JsonSerializerOptions.PropertyNamingPolicy = null;//解决后端传到前端变小写
                //空的字段不返回
                options.JsonSerializerOptions.IgnoreNullValues = true;
                //返回json小写
                options.JsonSerializerOptions.PropertyNamingPolicy = new LowercasePolicy();
                options.JsonSerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
                //options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);//解决后端返回数据中文被编码
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //自定义异常处理
            //app.UseMyExceptionHandler();

            //页面缓存
            //app.UseResponseCaching();

            //自己替换浏览器发送的请求头，强制浏览器使用页面缓存
            /*app.Use(async (ctx, next) =>
            {
                ctx.Request.GetTypedHeaders().CacheControl = new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
                {
                    Public = true,
                    MaxAge=TimeSpan.FromSeconds(120)
                };
                await next();
            });*/

            //初始化数据库以及初始数据
            Task.Run(async () =>
            {
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var menues = MenuHelper.GetMenues();
                    var dbService = scope.ServiceProvider.GetService<IDatabaseInit>();
                    await dbService.InitAsync(menues);
                }
            });

            //启用静态文件
            app.UseStaticFiles();

            //使用Cookie策略
            app.UseCookiePolicy();

            //使用https协议
            app.UseHttpsRedirection();

            //Session中间件
            app.UseSession();

            app.UseRouting();

            //用户认证
            app.UseAuthentication(); //鉴权，检测有没有登录，登录的是谁，赋值给User
            app.UseAuthorization(); //就是授权，检测权限

            //跨域请求设置 
            app.UseCors(builder => builder
            .AllowAnyOrigin() //允许任何来源
            .AllowAnyMethod() //所有请求方法
            .AllowAnyHeader()//所有请求头
            );

            //注意：使用Endpoints后，控制器无法使用[AllowAnonymous]过滤
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Login}/{id?}");

                endpoints.MapFallbackToController("Login", "Home");
            });

        }
    }
}
