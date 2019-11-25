using MediatR;
using SignalRChat.Domain.Features.Messages;
using SignalRChat.Infra.Results;
using System;
using System.Collections.Generic;

namespace SignalRChat.Applications.Features.Messages.Handlers
{
    public class MessagesCollection
    {
        public class Query : IRequest<Result<IEnumerable<Message>, Exception>> { }

        public class Handler : RequestHandler<Query, Result<IEnumerable<Message>, Exception>>
        {
            private readonly IMessageRepository _repository;

            public Handler(IMessageRepository repository)
            {
                _repository = repository;
            }

            protected override Result<IEnumerable<Message>, Exception> Handle(Query request)
            {
                return _repository.GetAll();
            }
        }
    }
}
