using SignalRChat.Domain.Base.Entities;

namespace SignalRChat.Domain.Features.Messages
{
    public class Message : Entity
    {
        public string Name { get; set; }
        public string Text { get; set; }
    }
}
