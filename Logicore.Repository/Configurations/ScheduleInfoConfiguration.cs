using Logicore.Core.Enities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logicore.Repository.Configurations
{
    /// <summary>
    /// 任务信息配置
    /// </summary>
    public class ScheduleInfoConfiguration : BaseConfiguration<ScheduleInfoEntity>
    {
        public override void Configure(EntityTypeBuilder<ScheduleInfoEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable("ScheduleInfo");
            builder.Property(e => e.CromExpress)
                    .IsRequired()
                    .HasColumnType("varchar(40)")
                    .HasDefaultValueSql("''");
            builder.Property(e => e.JobGroup)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("''");
            builder.Property(e => e.JobName)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasDefaultValueSql("''");
            builder.Property(e => e.RunStatus)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");
            builder.Property(e => e.TaskDescription).HasColumnType("varchar(200)");
            builder.Property(e => e.Token)
                    .IsRequired()
                    .HasColumnType("varchar(40)")
                    .HasDefaultValueSql("''");
        }
    }
}