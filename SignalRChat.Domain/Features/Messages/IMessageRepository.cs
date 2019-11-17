using SignalRChat.Infra.Results;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat.Domain.Features.Messages
{
    public interface IMessageRepository
    {
        Task<Result<Message, Exception>> AddMessage(Message message);
        Result<IQueryable<Message>, Exception> GetAll();
    }
}
