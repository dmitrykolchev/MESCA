// <copyright file="IMediatorService.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

namespace Xobex.Mediator;

public interface IMediatorService : IMediatorServiceBase
{
    /// <summary>
    /// Sends command to the back end
    /// </summary>
    /// <typeparam name="TRequest">Type of the request</typeparam>
    /// <param name="request">Requested command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>None</returns>
    Task SendAsync<TRequest>(TRequest request, CancellationToken cancellationToken)
        where TRequest : IRequest;

    /// <summary>
    /// Executes query
    /// </summary>
    /// <typeparam name="TResult">The query response type</typeparam>
    /// <param name="request">Request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Query result</returns>
    Task<TResult> QueryAsync<TResult>(IRequest<TResult> request, CancellationToken cancellationToken);

    /// <summary>
    /// Raises event
    /// </summary>
    /// <typeparam name="TEvent">The event type</typeparam>
    /// <param name="data">Event data</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>None</returns>
    Task RaiseAsync<TEvent>(TEvent data, CancellationToken cancellationToken)
        where TEvent : IEvent;
}
