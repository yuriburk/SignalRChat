using MongoDB.Driver;
using SignalRChat.Domain.Features.Messages;
using System.Collections.Generic;

namespace SignalRChat.Infra.NoSQL.Features.Messages
{
    public class MessageRepository
    {
        private readonly IMongoCollection<Message> _messages;

        public MessageRepository(ISignalRChatDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _messages = database.GetCollection<Message>(settings.MessagesCollectionName);
        }

        public List<Message> Get() =>
            _messages.Find(message => true).ToList();

        public Message Get(string id) =>
            _messages.Find(message => message.MongoId == id).FirstOrDefault();

        public Message Create(Message message)
        {
            _messages.InsertOne(message);
            return message;
        }

        public void Update(string id, Message messageIn) =>
            _messages.ReplaceOne(message => message.MongoId == id, messageIn);

        public void Remove(Message messageIn) =>
            _messages.DeleteOne(message => message.MongoId == messageIn.MongoId);

        public void Remove(string id) =>
            _messages.DeleteOne(message => message.MongoId == id);
    }
}
