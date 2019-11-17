using Microsoft.EntityFrameworkCore;
using SignalRChat.Domain.Features.Messages;
using SignalRChat.Domain.Features.Users;
using SignalRChat.Infra.Features.Messages;
using SignalRChat.Infra.Features.Users;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
