using FluentValidation;
using MediatR;

namespace Ste.Framework.Behaviour
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        //private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly IValidator<TRequest> _validator;

        public ValidationBehavior(IValidator<TRequest> validator)
        {
            _validator = validator;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request);
            return next().Result;
        }
    }
}
