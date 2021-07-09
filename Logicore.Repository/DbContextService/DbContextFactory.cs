using System;

namespace Logicore.Repository.DbContextService
{
    /// <summary>
    /// DbContext 工厂
    /// </summary>
    public class DbContextFactory : IDbContextFactory
    {
        public DbContextFactory()
        {

        }

        public AppDbContext CreateDbContext(DbContextType contextType)
        {
            string connStr = string.Empty;
            switch (contextType)
            {
                case DbContextType.Write:
                    connStr = MySqlConnect.GetWriteConnectString();
                    break;
                case DbContextType.Read:
                    connStr = MySqlConnect.GetReadConnectString();
                    break;

            }
            return new AppDbContext(connStr);
        }
    }
}