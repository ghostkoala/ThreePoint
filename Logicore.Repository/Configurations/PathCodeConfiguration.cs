using Logicore.Core.Enities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logicore.Repository.Configurations
{
    /// <summary>
    /// PathCode表信息配置
    /// </summary>
    public class PathCodeConfiguration : BaseConfiguration<PathCodeEntity>
    {
        public override void Configure(EntityTypeBuilder<PathCodeEntity> builder)
        {
            base.Configure(builder);
            builder.ToTable("PathCodes");
            builder.Property(e => e.Code).HasMaxLength(4).IsRequired();
            builder.Property(e => e.Len).IsRequired();
        }
    }
}