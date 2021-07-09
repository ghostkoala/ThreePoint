namespace Logicore.Repository.DbContextService
{
    /// <summary>
    /// DbContext 工厂接口
    /// </summary>
    public interface IDbContextFactory
    {
        /// <summary>
        /// 创建数据库接连上下文
        /// </summary>
        AppDbContext CreateDbContext(DbContextType contextType);
    }
    /// <summary>
    /// 读写类型
    /// </summary>
    public enum DbContextType
    {
        /// <summary>
        /// 写入操作
        /// </summary>
        Write,

        /// <summary>
        /// 读取操作
        /// </summary>
        Read
    }
}