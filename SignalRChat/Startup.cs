using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using MediatR;
using System;
using SignalRChat.Domain.Features.Messages;
using SignalRChat.Infra.Features.Messages;
using SignalRChat.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using SignalRChat.Applications.Features.Messages;
using Microsoft.AspNetCore.Http;

namespace SignalRChat
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MessageMappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            var assembly = AppDomain.CurrentDomain.Load("SignalRChat.Applications");
            services.AddMediatR(assembly);
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddDbContext<SignalRChatDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SignalRChat")));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSignalR();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<SignalRChatDbContext>();
                context.Database.EnsureCreated();
            }
        }
    }
}
