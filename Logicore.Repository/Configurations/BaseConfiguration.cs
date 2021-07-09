using Logicore.Core.Enities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logicore.Repository.Configurations
{
    /// <summary>
    /// Base信息配置
    /// </summary>
    public class BaseConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            //配置全局过滤器
            builder.HasQueryFilter(x => x.IsDeleted == false);

            //配置默认的主键和主键的数据长度
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(36);
            builder.Property(x => x.CreateUserId).HasMaxLength(36);
        }
    }
}