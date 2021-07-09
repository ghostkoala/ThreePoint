using System;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Logicore.Repository.Configurations;
using System.Linq;
using Logicore.Core.Enities;
using Logicore.Core.Exceptions;

namespace Logicore.Repository.DbContextService
{
    /// <summary>
    /// EF仓储服务上下文
    /// </summary>
    public class AppDbContext : DbContext
    {
        private string connString = string.Empty;

        /// <summary>
        /// 传入数据库链接字符串构造 AppDbContext
        /// </summary>
        /// <param name="conn">数据库链接字符串</param>
        public AppDbContext(string conn)
        {
            connString = conn;
            Database.EnsureCreated();
        }

        //添加EF操作日志打印到控制台，此为调试时使用
        //info级别
        //维护人员使用，交付时注释掉
        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
        {
            //builder.AddConsole();   //打印到控制台
        });

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.ApplyConfiguration<MenuEntity>(new MenuConfiguration());

            //添加FluentAPI配置
            var typesToRegister = typeof(SystemConfigConfiguration).Assembly.GetTypes()
                .Where(q => q.GetInterface(typeof(IEntityTypeConfiguration<>).FullName) != null && !q.FullName.StartsWith("Logicore.Repository.Configurations.BaseConfiguration"));

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //使用MySql数据库
            optionsBuilder.UseMySql(connString, mySqlOptionsAction: MySqloptions =>
            {
                MySqloptions.EnableRetryOnFailure(
                    maxRetryCount: 10,
                    maxRetryDelay: TimeSpan.FromSeconds(0),
                    errorNumbersToAdd: null);
            });

            //使用Sql Server数据库
            //optionsBuilder.UseSqlServer(connString);
        }

        #region DbSets

        /// <summary>
        /// 用户
        /// </summary>
        public DbSet<AdminEntity> Admins { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public DbSet<RoleEntity> Roles { get; set; }

        /// <summary>
        /// 菜单
        /// </summary>
        public DbSet<MenuEntity> Menus { get; set; }

        /// <summary>
        /// 角色菜单关系
        /// </summary>
        public DbSet<RoleMenuEntity> RoleMenus { get; set; }

        /// <summary>
        /// 路径码
        /// </summary>
        public DbSet<PathCodeEntity> PathCodes { get; set; }

        /// <summary>
        /// 登录日志
        /// </summary>
        public DbSet<LoginLogEntity> LoginLogs { get; set; }

        /// <summary>
        /// 系统配置
        /// </summary>
        public DbSet<SystemConfigEntity> SystemConfigs { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public DbSet<DepartmentEntity> Departments { get; set; }

        /// <summary>
        /// 站内信
        /// </summary>
        public DbSet<MessageEntity> Messages { get; set; }

        /// <summary>
        /// 站内信接收人
        /// </summary>
        public DbSet<MessageReceiverEntity> MessageReceivers { get; set; }

        /// <summary>
        /// 系统错误信息记录
        /// </summary>
        public DbSet<ServerExceptionEntity> serverExceptions { get; set; }

        #endregion

    }
}