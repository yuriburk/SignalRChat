using AutoMapper;
using SignalRChat.Applications.Features.Messages.Handlers;
using SignalRChat.Domain.Features.Messages;

namespace SignalRChat.Applications.Features.Messages
{
    public class MessageMappingProfile : Profile
    {
        public MessageMappingProfile()
        {
            CreateMap<MessagesCreate.Command, Message>();
        }
    }
}
