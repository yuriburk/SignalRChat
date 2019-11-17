using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SignalRChat.Domain.Features.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace SignalRChat.Infra.Features.Messages
{
    public class MessageEntityConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("Message");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Text).HasMaxLength(255).IsRequired();
            builder.HasOne(m => m.User).WithMany(u => u.Messages).HasForeignKey(m => m.UserId).IsRequired();
        }
    }
}
