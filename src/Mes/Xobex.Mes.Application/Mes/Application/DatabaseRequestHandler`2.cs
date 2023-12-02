// <copyright file="DatabaseRequestHandler`2.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.Extensions.Logging;
using Xobex.Data.Mes.Entities;
using Xobex.Mediator;

namespace Xobex.Data.Mes.Application;

public abstract class DatabaseRequestHandler<TRequest, TResult> : RequestHandler<TRequest, TResult>
    where TRequest : IRequest<TResult>
    where TResult : notnull
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="db">Database context from DI</param>
    /// <param name="logger">Logger from DI</param>
    protected DatabaseRequestHandler(
        IMesDbContext db,
        ILogger logger)
    {
        Db = db;
        Logger = logger;
    }

    /// <summary>
    /// Database context
    /// </summary>
    protected IMesDbContext Db { get; }

    /// <summary>
    /// Logger
    /// </summary>
    protected ILogger Logger { get; }
}
