using SignalRChat.Domain.Base;
using SignalRChat.Domain.Features.Messages;
using System.Collections.Generic;

namespace SignalRChat.Domain.Features.Users
{
    public class User : Entity
    {
        public string Name { get; set; }
        public List<Message> Messages { get; set; }
    }
}
