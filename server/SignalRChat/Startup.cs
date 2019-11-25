using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using SignalRChat.API.Extensions;
using SignalRChat.API.Hubs;
using SignalRChat.Infra.NoSQL;
using SignalRChat.Infra.NoSQL.Features.Messages;
using SimpleInjector;

namespace SignalRChat
{
    public class Startup
    {
        private static Container _container = new Container();
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSimpleInjectorDI(_container);
            _container.AddAutoMapper();
            _container.AddMediatR();
            services.AddSignalR();
            services.AddControllers();
            services.AddMvc();
            services.EnableSimpleInjectorCrossWiring(_container);
            services.UseSimpleInjectorAspNetRequestScoping(_container);

            services.AddSingleton<MessageRepository>();
            services.Configure<SignalRChatDatabaseSettings>(
                    Configuration.GetSection(nameof(SignalRChatDatabaseSettings)));

            services.AddSingleton<ISignalRChatDatabaseSettings>(sp =>
                    sp.GetRequiredService<IOptions<SignalRChatDatabaseSettings>>().Value);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            _container.RegisterMvcControllers(app);
            app.UseCors(builder =>
            {
                builder.AllowAnyHeader()
                       .AllowAnyMethod()
                       .SetIsOriginAllowed(_ => true)
                       .AllowCredentials();
            });
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("chathub");
            });
        }
    }
}
