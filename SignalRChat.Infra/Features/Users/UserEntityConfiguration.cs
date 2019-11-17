using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SignalRChat.Domain.Features.Users;

namespace SignalRChat.Infra.Features.Users
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Name).HasMaxLength(255).IsRequired();
        }
    }
}
