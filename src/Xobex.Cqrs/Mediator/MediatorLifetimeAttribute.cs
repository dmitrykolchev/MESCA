// <copyright file="MediatorLifetimeAttribute.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.Extensions.DependencyInjection;

namespace Xobex.Mediator;

[AttributeUsage(AttributeTargets.Class)]
public class MediatorLifetimeAttribute : Attribute
{
    public MediatorLifetimeAttribute(ServiceLifetime lifetime)
    {
        Lifetime = lifetime;
    }

    public ServiceLifetime Lifetime { get; }
}
