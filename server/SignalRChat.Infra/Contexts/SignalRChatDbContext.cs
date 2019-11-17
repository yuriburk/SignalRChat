using Microsoft.EntityFrameworkCore;
using SignalRChat.Domain.Features.Annotations;
using SignalRChat.Infra.Features.Annotations;

namespace SignalRChat.Infra.Contexts
{
    public class SignalRChatDbContext : DbContext
    {
        public SignalRChatDbContext(DbContextOptions<SignalRChatDbContext> options) : base(options)
        {

        }

        public DbSet<Annotation> Annotations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AnnotationEntityConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseLazyLoadingProxies(false);
    }
}
