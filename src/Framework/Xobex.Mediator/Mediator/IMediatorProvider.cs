// <copyright file="IMediatorProvider.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

namespace Xobex.Mediator;

public interface IMediatorProvider
{
    void Add(HandlerDesriptor desriptor);

    IRequestHandler GetRequestHandler(IServiceProvider serviceProvider, Type requestType);

    IReadOnlyList<IRequestPostProcessor> GetRequestPostProcessors(IServiceProvider services, Type requestType);

    IReadOnlyList<IEventHandler> GetEventHandlers(IServiceProvider serviceProvider, Type notificationType);

    IReadOnlyList<IValidator> GetValidators(IServiceProvider serviceProvider, Type requestType);
}
