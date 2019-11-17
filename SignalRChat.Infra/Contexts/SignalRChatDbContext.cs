using Microsoft.EntityFrameworkCore;
using SignalRChat.Domain.Features.Messages;
using SignalRChat.Domain.Features.Users;
using SignalRChat.Infra.Features.Messages;
using SignalRChat.Infra.Features.Users;

namespace SignalRChat.Infra.Contexts
{
    public class SignalRChatDbContext : DbContext
    {
        public SignalRChatDbContext(DbContextOptions<SignalRChatDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new MessageEntityConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseLazyLoadingProxies(false);
    }
}
