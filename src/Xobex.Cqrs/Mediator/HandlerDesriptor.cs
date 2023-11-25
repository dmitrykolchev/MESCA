// <copyright file="HandlerDesriptor.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.Extensions.DependencyInjection;

namespace Xobex.Mediator;

public class HandlerDesriptor
{
    public Type ContractType { get; set; } = null!;

    public Type? HandlerType { get; set; }   

    public object? Instance { get; set; }

    public ServiceLifetime ServiceLifetime { get; set; }
}
