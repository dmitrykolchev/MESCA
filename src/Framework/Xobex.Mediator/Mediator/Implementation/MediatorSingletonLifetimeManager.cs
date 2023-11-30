// <copyright file="MediatorSingletonLifetimeManager.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

namespace Xobex.Mediator.Implementation;

internal class MediatorSingletonLifetimeManager(IServiceProvider serviceProvider) : MediatorLifetimeManager(serviceProvider)
{
}
