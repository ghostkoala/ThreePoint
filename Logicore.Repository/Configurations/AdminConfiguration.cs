using Logicore.Core.Enities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logicore.Repository.Configurations
{
    /// <summary>
    /// Admin表信息配置
    /// </summary>
    public class AdminConfiguration : BaseConfiguration<AdminEntity>
    {
        public override void Configure(EntityTypeBuilder<AdminEntity> builder)
        {
            base.Configure(builder);
            builder.ToTable("Admins");
            builder.Property(e => e.LoginName).HasMaxLength(20).IsRequired();
            builder.Property(e => e.RealName).IsUnicode(true).HasMaxLength(20).IsRequired();
            builder.Property(e => e.Email).HasMaxLength(36).IsRequired();
            builder.Property(e => e.Password).HasMaxLength(50).IsRequired();
            builder.Property(e => e.DepartmentId).HasMaxLength(36);
            builder.HasOne(e => e.Department).WithMany(e => e.Admins).HasForeignKey(e => e.DepartmentId);
            builder.HasOne(e => e.Role).WithMany(e => e.Admins).HasForeignKey(e => e.RoleId);
        }
    }
}