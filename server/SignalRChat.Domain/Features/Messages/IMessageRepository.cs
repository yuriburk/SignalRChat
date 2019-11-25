using SignalRChat.Infra.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignalRChat.Domain.Features.Messages
{
    public interface IMessageRepository
    {
        Task<Result<Message, Exception>> Add(Message message);
        Result<IEnumerable<Message>, Exception> GetAll();
    }
}
