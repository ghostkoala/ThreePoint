using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Logicore.Core.Enities;
using Logicore.Core.Enums;
using Logicore.Core.Exceptions;
using Logicore.Core.Extensions;
using Logicore.Core.ServerModels;
using Logicore.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Logicore.Repository.DbContextService
{
    public class DatabaseInit : IDatabaseInit
    {
        private readonly IDbContextFactory _dbContextFactory;
        private readonly ILogger<DatabaseInit> _logger;
        private readonly IMenuRepository _menuRepository;

        public DateTime Now => new DateTime(2016, 06, 06, 0, 0, 0);

        /// <summary>
        /// ctor
        /// </summary>
        public DatabaseInit(IDbContextFactory dbContextFactory, ILogger<DatabaseInit> logger, IMenuRepository menuRepository)
        {
            _dbContextFactory = dbContextFactory;
            _logger = logger;
            _menuRepository = menuRepository;
        }

        public async Task<bool> InitAsync(List<MenuDto> menues)
        {
            var dbContext = _dbContextFactory.CreateDbContext(DbContextType.Write);
            try
            {
                await dbContext.Database.MigrateAsync();
                if (await dbContext.SystemConfigs.AnyAsync(item => item.IsDataInited)) return false;

                #region 用户

                var admin = new AdminEntity
                {
                    Id = Guid.NewGuid().ToString("N"),
                    LoginName = "jucheap",
                    RealName = "超级管理员",
                    Password = "123456".ToMd5(),
                    Email = "service@jucheap.com",
                    CreateDateTime = Now
                };
                var guest = new AdminEntity
                {
                    Id = Guid.NewGuid().ToString("N"),
                    LoginName = "admin",
                    RealName = "游客",
                    Password = "123456".ToMd5(),
                    Email = "service@jucheap.com",
                    CreateDateTime = Now
                };
                //用户
                var user = new List<AdminEntity>
                {
                    admin,
                    guest
                };
                #endregion

                #region 菜单

                await _menuRepository.ReInitMenuesAsync(menues);
                var menus = await dbContext.Menus.ToListAsync();
                #endregion

                #region 角色

                var superAdminRole = new RoleEntity
                {
                    Id = Guid.NewGuid().ToString("N"),
                    Name = "超级管理员",
                    Description = "超级管理员",
                    CreateDateTime = Now
                };
                var guestRole = new RoleEntity
                {
                    Id = Guid.NewGuid().ToString("N"),
                    Name = "guest",
                    Description = "游客",
                    CreateDateTime = Now
                };
                var roles = new List<RoleEntity>(){
                    superAdminRole,
                    guestRole
                };

                #endregion

                #region 部门

                var Admindepartment = new DepartmentEntity()
                {
                    Id = Guid.NewGuid().ToString("N"),
                    CreateDateTime = Now,
                    Name = "管理部门",
                    FullName = "管理部门",
                    ParentId = ""
                };

                var Testepartment = new DepartmentEntity()
                {
                    Id = Guid.NewGuid().ToString("N"),
                    CreateDateTime = Now,
                    Name = "管理部门 - 测试部门",
                    FullName = "测试部门",
                    ParentId = Admindepartment.Id
                };

                #endregion


                #region 用户字段配置
                admin.Role = superAdminRole;
                admin.Department = Admindepartment;
                guest.Role = guestRole;
                guest.Department = Testepartment;
                #endregion

                #region 角色菜单权限关系

                var roleMenus = new List<RoleMenuEntity>();
                //guest授权(guest只有查看权限，没有按钮操作权限)
                menus.Where(item => item.Type != MenuType.Action).ToList().ForEach
                    (m =>
                {
                    roleMenus.Add(new RoleMenuEntity
                    {
                        Id = Guid.NewGuid().ToString("N"),
                        RoleId = guestRole.Id,
                        MenuId = m.Id,
                        CreateDateTime = Now
                    });
                });

                #endregion

                #region 系统配置

                var systemConfig = new SystemConfigEntity
                {
                    Id = Guid.NewGuid().ToString("N"),
                    SystemName = "JuCheap Core",
                    IsDataInited = true,
                    DataInitedDate = Now,
                    CreateDateTime = Now,
                    IsDeleted = false
                };

                #endregion

                dbContext.Roles.AddRange(roles);
                dbContext.Admins.AddRange(user);
                dbContext.RoleMenus.AddRange(roleMenus);
                dbContext.SystemConfigs.Add(systemConfig);
                dbContext.Departments.Add(Admindepartment);
                dbContext.Departments.Add(Testepartment);
                await dbContext.SaveChangesAsync();
                //await InitPathCodeAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new ServerException("err", ex, 500);
            }
        }

        public async Task<bool> InitPathCodeAsync()
        {
            var dbContext = _dbContextFactory.CreateDbContext(DbContextType.Write);
            //生成路径码
            var codes = new List<string>(26);
            for (var i = 65; i <= 90; i++)
            {
                codes.Add(((char)i).ToString());
            }
            //求组合
            var list = (from a in codes
                        from b in codes
                        select new PathCodeEntity
                        {
                            Code = a + b,
                            Len = 2
                        }).OrderBy(item => item.Code).ToList();
            list.ForEach(x => x.Init());
            await dbContext.Database.ExecuteSqlRawAsync("DELETE FROM PathCodes");
            dbContext.PathCodes.AddRange(list);

            return await dbContext.SaveChangesAsync() > 0;
        }
    }
}