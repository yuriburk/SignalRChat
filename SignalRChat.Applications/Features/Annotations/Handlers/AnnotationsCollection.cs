using MediatR;
using SignalRChat.Domain.Features.Annotations;
using SignalRChat.Infra.Results;
using System;
using System.Linq;

namespace SignalRChat.Applications.Features.AnnotationsSolicitations.Handlers
{
    public class AnnontationsCollection
    {
        public class Query : IRequest<Result<IQueryable<Annotation>, Exception>> { }

        public class Handler : RequestHandler<Query, Result<IQueryable<Annotation>, Exception>>
        {
            private readonly IAnnotationRepository _repository;

            public Handler(IAnnotationRepository repository)
            {
                _repository = repository;
            }

            protected override Result<IQueryable<Annotation>, Exception> Handle(Query request)
            {
                return _repository.GetAll();
            }
        }
    }
}
