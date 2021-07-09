using Logicore.IRepository;
using Logicore.IServices;
using Logicore.Repository;
using Logicore.Repository.DbContextService;
using Logicore.Services;
using Logicore.Web.Servers;
using Microsoft.Extensions.DependencyInjection;

namespace Logicore.Web.Extensions
{
    /// <summary>
    /// 系统数据服务
    /// </summary>
    public static class DataServiceExtension
    {
        /// <summary>
        /// 注入数据
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddDataService(this IServiceCollection services)
        {
            #region 依赖注入
            services.AddScoped<IDatabaseInit, DatabaseInit>();
            services.AddScoped<IDbContextFactory, DbContextFactory>();
            services.AddScoped<IServiceFactory, ServiceFactory>();

            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IPathCodeRepository, PathCodeRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IServerExceptionRepository, ServerExceptionRepository>();

            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IServerExceptionService, ServerExceptionService>();
            #endregion
            return services;
        }
    }
}