using AutoMapper;
using Microsoft.Extensions.Configuration;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SignalRChat.Applications.Features.Messages;
using SignalRChat.Domain.Features.Messages;
using SignalRChat.Infra.Contexts;
using SignalRChat.Infra.Features.Messages;
using System;
using Microsoft.AspNetCore.Builder;

namespace SignalRChat.API.Extensions
{
    public static class ConfigurationExtensions
    {
        public static void AddAutoMapper(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MessageMappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        public static void AddMediatR(this IServiceCollection services)
        {
            var assembly = AppDomain.CurrentDomain.Load("SignalRChat.Applications");
            services.AddMediatR(assembly);
        }

        public static void AddDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
        
        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SignalRChatDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("SignalRChat")));
        }

        public static void EnsureCreatedDbContext(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<SignalRChatDbContext>();
                context.Database.EnsureCreated();
            }
        }
    }
}
