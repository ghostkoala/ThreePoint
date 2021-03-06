using ThreePoint.Core.Quartz;
using ThreePoint.IRepository;
using ThreePoint.IServices;
using ThreePoint.Repository;
using ThreePoint.Repository.DbContextService;
using ThreePoint.Services;
using ThreePoint.Web.Servers;
using Microsoft.Extensions.DependencyInjection;

namespace ThreePoint.Web.Extensions
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
            services.AddScoped<IScheduleCenter, ScheduleCenter>();

            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IPathCodeRepository, PathCodeRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IServerExceptionRepository, ServerExceptionRepository>();
            services.AddScoped<ISystemConfigRepository, SystemConfigRepository>();
            services.AddScoped<IScheduleInfoRepository, ScheduleInfoRepository>();

            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IServerExceptionService, ServerExceptionService>();
            services.AddScoped<ISystemConfigService, SystemConfigService>();
            services.AddScoped<IScheduleInfoService, ScheduleInfoService>();

            #endregion
            return services;
        }
    }
}