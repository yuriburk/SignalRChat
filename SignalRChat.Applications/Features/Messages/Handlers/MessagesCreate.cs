using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using SignalRChat.Domain.Features.Messages;
using SignalRChat.Domain.Results;
using SignalRChat.Infra.Results;
using System.Threading;
using System.Threading.Tasks;

namespace SignalRChat.Applications.Features.Messages.Handlers
{
    [AutoMap(typeof(Message))]
    public class MessagesCreate
    {
        public class Command : IRequest<Result<int, Error>>
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

        public class Handler : IRequestHandler<Command, Result<int, Error>>
        {
            private readonly IMapper _mapper;
            private readonly IMessageRepository _repository;

            public Handler(IMapper mapper, IMessageRepository repository)
            {
                _mapper = mapper;
                _repository = repository;
            }

            public async Task<Result<int, Error>> Handle(Command request, CancellationToken cancellationToken)
            {
                var message = await _repository.AddMessage(_mapper.Map<Message>(request));
                if (message.IsFailure)
                    return message.Failure;

                return message.Success.Id;
            }
        }
    }
}
