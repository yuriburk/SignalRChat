using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SignalRChat.Domain.Base.Entities
{
    public abstract class Entity : IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public virtual string MongoId { get; set; }

        public virtual int Id { get; set; }

        public bool Validate()
        {
            return true;
        }
    }
}
