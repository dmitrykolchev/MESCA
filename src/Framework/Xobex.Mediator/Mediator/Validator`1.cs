// <copyright file="ValidateHandler`1.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

namespace Xobex.Mediator;

public abstract class Validator<TRequest> : IValidator<TRequest>
    where TRequest : IRequest
{
    protected Validator() { }

    public virtual int Ordinal => 0;

    public abstract Task ValidateAsync(TRequest request, CancellationToken cancellationToken);

    Task IValidator.ValidateAsync(IRequest request, CancellationToken cancellationToken)
    {
        return ValidateAsync((TRequest)request, cancellationToken);
    }
}
