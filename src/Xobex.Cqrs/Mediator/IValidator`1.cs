// <copyright file="IValidator`1.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

namespace Xobex.Mediator;

public interface IValidator<TRequest> : IValidator
    where TRequest : IRequest
{
    Task ValidateAsync(TRequest request, CancellationToken cancellationToken);
}

