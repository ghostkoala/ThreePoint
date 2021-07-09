using Logicore.Core.Enities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logicore.Repository.Configurations
{
    /// <summary>
    ///LoginLog表信息配置
    /// </summary>
    public class LoginLogConfiguration : BaseConfiguration<LoginLogEntity>
    {
        public override void Configure(EntityTypeBuilder<LoginLogEntity> builder)
        {
            base.Configure(builder);
            builder.ToTable("LoginLogs");
            builder.Property(e => e.LoginName).HasMaxLength(20).IsRequired();
            builder.Property(e => e.IP).HasMaxLength(20).IsRequired();
            builder.Property(e => e.Message).IsUnicode(true).HasMaxLength(200).IsRequired();
            builder.Property(e => e.UserId).HasMaxLength(36).IsRequired();
        }
    }
}