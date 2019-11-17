using SignalRChat.Domain.Base.Entities;
using SignalRChat.Domain.Features.Users;

namespace SignalRChat.Domain.Features.Messages
{
    public class Message : Entity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public string Text { get; set; }
    }
}
