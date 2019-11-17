using SignalRChat.Domain.Base.Entities;

namespace SignalRChat.Domain.Features.Annotations
{
    public class Annotation : Entity
    {
        public string Name { get; set; }
        public string Text { get; set; }
    }
}
