using ThreePoint.Core.Enities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ThreePoint.Repository.Configurations
{
    /// <summary>
    /// 站内信表信息配置
    /// </summary>
    public class MessageConfiguration : BaseConfiguration<MessageEntity>
    {
        public override void Configure(EntityTypeBuilder<MessageEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable("Messages");
            builder.Property(x => x.Title).IsRequired().IsUnicode(true).HasMaxLength(50);
            builder.Property(x => x.Contents).IsRequired().IsUnicode(true).HasMaxLength(500);
            builder.HasMany(x => x.MessageReceivers).WithOne(x => x.Message).HasForeignKey(x => x.MessageId);
        }
    }
}