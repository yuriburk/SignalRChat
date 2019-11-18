using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using SignalRChat.Domain.Features.Annotations;
using SignalRChat.Infra.Results;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SignalRChat.Applications.Features.Annotations.Handlers
{
    [AutoMap(typeof(Annotation))]
    public class AnnotationsCreate
    {
        public class Command : IRequest<Result<Annotation, Exception>>
        {
            public string Name { get; set; }
            public string Text { get; set; }

            public virtual ValidationResult Validate()
            {
                return new Validator().Validate(this);
            }

            class Validator : AbstractValidator<Command>
            {
                public Validator()
                {
                    RuleFor(c => c.Name).NotEmpty().MaximumLength(255);
                    RuleFor(c => c.Text).NotEmpty().MaximumLength(255);
                }
            }
        }

        public class Handler : IRequestHandler<Command, Result<Annotation, Exception>>
        {
            private readonly IMapper _mapper;
            private readonly IAnnotationRepository _annotationRepository;

            public Handler(IMapper mapper, IAnnotationRepository annotationRepository)
            {
                _mapper = mapper;
                _annotationRepository = annotationRepository;
            }

            public async Task<Result<Annotation, Exception>> Handle(Command request, CancellationToken cancellationToken)
            {
                var annotation = _mapper.Map<Annotation>(request);
                return await _annotationRepository.AddAnnotation(annotation);
            }
        }
    }
}
