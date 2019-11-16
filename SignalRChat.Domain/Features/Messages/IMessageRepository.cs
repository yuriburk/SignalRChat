using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SignalRChat.Domain.Features.Messages
{
    public interface IMessageRepository
    {
        Task<Tuple<bool, List<string>>> AddUser(Message message);
        Task<Tuple<Message, List<string>>> GetUser(Expression<Func<Message, bool>> predicate);
    }
}
