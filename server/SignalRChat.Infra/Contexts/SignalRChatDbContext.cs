using Microsoft.EntityFrameworkCore;
using SignalRChat.Domain.Features.Messages;
using SignalRChat.Infra.Features.Messages;

namespace SignalRChat.Infra.Contexts
{
    public class SignalRChatDbContext : DbContext
    {
        public SignalRChatDbContext(DbContextOptions<SignalRChatDbContext> options) : base(options)
        {

        }

        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MessageEntityConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseLazyLoadingProxies(false);
    }
}
