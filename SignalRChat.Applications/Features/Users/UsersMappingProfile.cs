using AutoMapper;
using SignalRChat.Applications.Features.Users.Handlers;
using SignalRChat.Domain.Features.Users;

namespace SignalRChat.Applications.Features.Users
{
    public class UsersMappingProfile : Profile
    {
        public UsersMappingProfile()
        {
            CreateMap<GetOrAddUser.Query, User>();
        }
    }
}
