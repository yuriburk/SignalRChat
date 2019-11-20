using FluentValidation;
using MediatR;
using SignalRChat.Infra.Results;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SignalRChat.API.Flows
{
    public class ValidationFlow<TRequest, TResponse> : IPipelineBehavior<TRequest, Result<TResponse, Exception>>
        where TRequest : IRequest<Result<TResponse, Exception>>
    {
        private readonly IValidator<TRequest>[] _validators;

        public ValidationFlow(IValidator<TRequest>[] validators)
        {
            _validators = validators;
        }

        public async Task<Result<TResponse, Exception>> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<Result<TResponse, Exception>> next)
        {
            var failures = _validators
            .Select(v => v.Validate(request))
            .SelectMany(result => result.Errors)
            .Where(f => !(f is null))
            .ToList();

            return failures.Any()
                ? new ValidationException(failures)
                : await next();
        }
    }
}
