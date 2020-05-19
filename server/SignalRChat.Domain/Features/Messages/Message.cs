using SignalRChat.Domain.Base.Entities;
using System;

namespace SignalRChat.Domain.Features.Messages
{
    public class Message : Entity
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}
