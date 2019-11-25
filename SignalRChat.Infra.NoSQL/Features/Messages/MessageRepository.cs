using MongoDB.Driver;
using SignalRChat.Domain.Features.Messages;
using SignalRChat.Infra.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignalRChat.Infra.NoSQL.Features.Messages
{
    public class MessageRepository : IMessageRepository
    {
        private readonly IMongoCollection<Message> _messages;

        public MessageRepository(ISignalRChatDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _messages = database.GetCollection<Message>(settings.MessagesCollectionName);
        }

        public Result<IEnumerable<Message>, Exception> GetAll()
        => _messages.Find(message => true).ToList();

        public Message Get(string id) =>
            _messages.Find(message => message.MongoId == id).FirstOrDefault();

        public async Task<Result<Message, Exception>> Add(Message message)
        {
            await _messages.InsertOneAsync(message);
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
