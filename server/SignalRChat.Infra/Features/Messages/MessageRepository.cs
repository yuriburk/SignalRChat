using SignalRChat.Domain.Features.Messages;
using SignalRChat.Infra.Contexts;
using SignalRChat.Infra.Results;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat.Infra.Features.Messages
{
    public class MessageRepository : IMessageRepository
    {
        SignalRChatDbContext _context;

        public MessageRepository(SignalRChatDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Message, Exception>> AddMessage(Message message)
        {
            var newMessage = _context.Messages.Add(message).Entity;
            await _context.SaveChangesAsync();

            return newMessage;
        }

        public Result<IQueryable<Message>, Exception> GetAll()
        {
            return _context.Messages;
        }
    }
}