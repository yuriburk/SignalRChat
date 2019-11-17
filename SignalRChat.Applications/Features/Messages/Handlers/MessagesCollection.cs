using MediatR;
using SignalRChat.Domain.Features.Messages;
using SignalRChat.Domain.Results;
using SignalRChat.Infra.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SignalRChat.Applications.Features.MessagesSolicitations.Handlers
{
    public class MessagesCollection
    {
        public class Query : IRequest<Result<IQueryable<Message>, Error>> { }

        public class Handler : RequestHandler<Query, Result<IQueryable<Message>, Error>>
        {
            private readonly IMessageRepository _repository;

            public Handler(IMessageRepository repository)
            {
                _repository = repository;
            }

            protected override Result<IQueryable<Message>, Error> Handle(Query request)
            {
                return _repository.GetAll();
            }
        }
    }
}
