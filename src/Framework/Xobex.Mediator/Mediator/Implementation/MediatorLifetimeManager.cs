// <copyright file="MediatorLifetimeManager.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;

namespace Xobex.Mediator.Implementation;

internal class MediatorLifetimeManager(IServiceProvider serviceProvider) : IMediatorLifetimeManager
{
    private readonly IServiceProvider _serviceProvider = serviceProvider 
        ?? throw new ArgumentNullException(nameof(serviceProvider));
    private readonly ConcurrentDictionary<Type, object> _handlers = new();

    public object GetOrCreate(Type handlerType)
    {
        return _handlers.GetOrAdd(handlerType, t => ActivatorUtilities.CreateInstance(_serviceProvider, t));
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            foreach (object item in _handlers.Values)
            {
                if (item is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }
        }
    }

    void IDisposable.Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
