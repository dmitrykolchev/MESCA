﻿// <copyright file="Request.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

namespace Xobex.Mediator;

public abstract class Request<TResult> : IRequest<TResult>
{
    protected Request() { }
}

