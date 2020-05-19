using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SignalRChat.Domain.Base.Entities
{
    public abstract class Entity : IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public virtual string Id { get; set; }

        public virtual bool Validate()
        {
            return true;
        }
    }
}
