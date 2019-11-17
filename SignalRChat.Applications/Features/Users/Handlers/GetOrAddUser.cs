using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using SignalRChat.Domain.Features.Users;
using SignalRChat.Infra.Results;
using System;
using System.Threading.Tasks;

namespace SignalRChat.Applications.Features.Users.Handlers
{
    public class GetOrAddUser
    {
        public class Query : IRequest<Task<Result<User, Exception>>>
        {
            public int UserId { get; set; }
            public string UserName { get; set; }

            public virtual ValidationResult Validate()
            {
                return new Validator().Validate(this);
            }

            class Validator : AbstractValidator<Query>
            {
                public Validator()
                {
                    RuleFor(c => c.UserName).NotEmpty();
                }
            }
        }

        public class Handler : RequestHandler<Query, Task<Result<User, Exception>>>
        {
            private readonly IMapper _mapper;
            private readonly IUserRepository _repository;

            public Handler(IMapper mapper, IUserRepository repository)
            {
                _mapper = mapper;
                _repository = repository;
            }

            protected override async Task<Result<User, Exception>> Handle(Query request)
            {
                var user = _mapper.Map<User>(request);
               var callback = await _repository.GetUser(x => x.Id == user.Id);

                if (callback.IsFailure)
                    return await _repository.AddUser(user);

                return callback.Success;
            }
        }
    }
}
