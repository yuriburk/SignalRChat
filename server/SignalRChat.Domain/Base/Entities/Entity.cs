namespace SignalRChat.Domain.Base.Entities
{
    public abstract class Entity : IEntity
    {
        public virtual int Id { get; set; }

        public bool Validate()
        {
            return true;
        }
    }
}
