﻿using AutoMapper;
using Microsoft.Extensions.Configuration;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SignalRChat.Applications.Features.Messages;
using SignalRChat.Domain.Features.Messages;
using SignalRChat.Infra.Contexts;
using SignalRChat.Infra.Features.Messages;
using SignalRChat.API.Flows;
using FluentValidation;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using Microsoft.AspNetCore.Mvc.Controllers;
using SimpleInjector.Integration.AspNetCore.Mvc;
using SignalRChat.Applications;
using System.Reflection;
using System.Collections.Generic;
using SignalRChat.Applications.Features.Messages.Handlers;
using SignalRChat.Infra.NoSQL.Features.Messages;

namespace SignalRChat.API.Extensions
{
    public static class ConfigurationExtensions
    {
        public static void AddAutoMapper(this Container container)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MessageMappingProfile());
            });
            container.RegisterInstance(mappingConfig);
            container.Register(() => mappingConfig.CreateMapper(container.GetInstance));
        }

        public static void AddMediatR(this Container container)
        {
            var assemblies = GetAssemblies();

            container.RegisterSingleton<IMediator, Mediator>();
            container.Register(() => new ServiceFactory(container.GetInstance), Lifestyle.Singleton);
            container.Register(typeof(IRequestHandler<,>), assemblies);
            container.Register(typeof(IRequestHandler<>), assemblies);
            container.Collection.Register(typeof(IPipelineBehavior<,>), new[]
            {
                typeof(ValidationFlow<,>)
            });
        }

        public static void AddSimpleInjectorDI(this IServiceCollection services, Container container)
        {
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            services.AddSingleton<IControllerActivator>(new SimpleInjectorControllerActivator(container));
            services.UseSimpleInjectorAspNetRequestScoping(container);
            container.Register<IMessageRepository, Infra.Features.Messages.MessageRepository>();
            container.Register<IHttpContextAccessor, HttpContextAccessor>();
            container.Collection.Register(typeof(IValidator<>), typeof(ApplicationModule).GetTypeInfo().Assembly);
        }

        public static void AddEntityFramework(this Container container, IConfiguration configuration)
        {
            var options = new DbContextOptionsBuilder<SignalRChatDbContext>().UseNpgsql(configuration.GetConnectionString("SignalRChat")).Options;
            container.Register(() =>
            {
                return new SignalRChatDbContext(options);
            }, Lifestyle.Scoped);

            using (var context = new SignalRChatDbContext(options))
            {
                context.Database.EnsureCreated();
            }
        }

        private static IEnumerable<Assembly> GetAssemblies()
        {
            yield return typeof(IMediator).GetTypeInfo().Assembly;
            yield return typeof(MessagesCollection).GetTypeInfo().Assembly;
        }
    }
}
