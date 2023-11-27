// <copyright file="VerifyInitializedBehavior.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Xobex.Mediator;

namespace Xobex.Mes.Application;

public class VerifyInitializedBehavior<TEntity, TResult> : Behavior<TResult>
    where TEntity : class
{
    private readonly IMesDbContext _context;
    private readonly ILogger _logger;

    public VerifyInitializedBehavior(IMesDbContext context, ILogger<VerifyInitializedBehavior<TEntity, TResult>> logger)
    {
        _context = context;
        _logger = logger;
    }

    public override async Task<TResult?> ProcessAsync(IRequest<TResult> request, Func<Task<TResult>>? next, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(next);
        _logger.LogInformation($"Verify {typeof(TEntity)} is empty");
        if (!await _context.Set<TEntity>().AnyAsync())
        {
            _logger.LogInformation($"{typeof(TEntity)} is empty move to next step");
            return await next();
        }
        else
        {
            _logger.LogInformation($"{typeof(TEntity)} already initialized");
        }
        return default;
    }
}
