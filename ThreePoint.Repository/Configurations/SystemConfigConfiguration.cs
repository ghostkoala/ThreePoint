using ThreePoint.Core.Enities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ThreePoint.Repository.Configurations
{
    /// <summary>
    /// SystemConfig表信息配置
    /// </summary>
    public class SystemConfigConfiguration : BaseConfiguration<SystemConfigEntity>
    {
        public override void Configure(EntityTypeBuilder<SystemConfigEntity> builder)
        {
            base.Configure(builder);
            builder.Property(e => e.SystemName).IsUnicode(true).HasMaxLength(50).IsRequired();
            builder.Property(e => e.IsDataInited).IsRequired();
            builder.Property(e => e.DataInitedDate).IsRequired();
            builder.Property(e => e.CreateDateTime).IsRequired();
            builder.Property(e => e.IsDeleted).IsRequired();
            builder.ToTable("SystemConfigs");
        }
    }
}