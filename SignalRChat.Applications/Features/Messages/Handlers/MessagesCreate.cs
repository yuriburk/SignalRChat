using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using SignalRChat.Domain.Features.Messages;
using SignalRChat.Infra.Results;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SignalRChat.Applications.Features.Messages.Handlers
{
    [AutoMap(typeof(Message))]
    public class MessagesCreate
    {
        public class Command : IRequest<Result<Message, Exception>>
        {
            public int Name { get; set; }
            public string Text { get; set; }

            public virtual ValidationResult Validate()
            {
                return new Validator().Validate(this);
            }

            class Validator : AbstractValidator<Command>
            {
                public Validator()
                {
                    RuleFor(c => c.Name).NotEmpty().WithMessage("É necessário estar logado para enviar uma mensagem.");
                    RuleFor(c => c.Text).NotEmpty().MaximumLength(255);
                }
            }
        }

        public class Handler : IRequestHandler<Command, Result<Message, Exception>>
        {
            private readonly IMapper _mapper;
            private readonly IMessageRepository _messageRepository;

            public Handler(IMapper mapper, IMessageRepository messageRepository)
            {
                _mapper = mapper;
                _messageRepository = messageRepository;
            }

            public async Task<Result<Message, Exception>> Handle(Command request, CancellationToken cancellationToken)
            {
                var message = _mapper.Map<Message>(request);

                var callback = await _messageRepository.AddMessage(message);
                if (callback.IsFailure)
                    return callback.Failure;
                return callback.Success;
            }
        }
    }
}
