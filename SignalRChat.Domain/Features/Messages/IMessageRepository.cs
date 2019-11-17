using SignalRChat.Domain.Results;
using SignalRChat.Infra.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SignalRChat.Domain.Features.Messages
{
    public interface IMessageRepository
    {
        Task<Result<Message, Error>> AddMessage(Message message);
        Result<IQueryable<Message>, Error> GetAll();
    }
}
