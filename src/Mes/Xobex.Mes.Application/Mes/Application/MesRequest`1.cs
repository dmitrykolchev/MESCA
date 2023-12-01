// <copyright file="MesRequest`1.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Xobex.Data.Enums;
using Xobex.Mediator;

namespace Xobex.Mes.Application;

public abstract class MesRequest<TResult> : Request<TResult>
{
    protected MesRequest() { }
    public abstract RequestVerbs Verb { get; }
}
