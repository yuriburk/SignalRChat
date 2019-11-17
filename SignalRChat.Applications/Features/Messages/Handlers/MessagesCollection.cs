using MediatR;
using SignalRChat.Domain.Features.Messages;
using SignalRChat.Infra.Results;
using System;
using System.Linq;

namespace SignalRChat.Applications.Features.MessagesSolicitations.Handlers
{
    public class MessagesCollection
    {
        public class Query : IRequest<Result<IQueryable<Message>, Exception>> { }

        public class Handler : RequestHandler<Query, Result<IQueryable<Message>, Exception>>
        {
            private readonly IMessageRepository _repository;

            public Handler(IMessageRepository repository)
            {
                _repository = repository;
            }

            protected override Result<IQueryable<Message>, Exception> Handle(Query request)
            {
                return _repository.GetAll();
            }
        }
    }
}
