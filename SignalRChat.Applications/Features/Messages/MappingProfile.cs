using AutoMapper;
using SignalRChat.Applications.Features.Messages.Handlers;
using SignalRChat.Domain.Features.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace SignalRChat.Applications.Features.Messages
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<MessagesCreate.Command, Message>();
        }
    }
}
