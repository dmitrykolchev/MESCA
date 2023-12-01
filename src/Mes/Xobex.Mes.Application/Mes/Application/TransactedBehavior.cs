// <copyright file="TransactedBehavior.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.Extensions.Logging;
using Xobex.Data.EntityFramework;
using Xobex.Mediator;

namespace Xobex.Mes.Application;

public class TransactedBehavior : Behavior
{
    private readonly IMesDbContext _context;
    private readonly ILogger _logger;

    public TransactedBehavior(IMesDbContext context, ILogger<TransactedBehavior> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public override async Task<object?> ProcessAsync(IRequest request, Func<Task<object>>? next, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(next);

        using ITransactionWrapper transaction = _context.BeginTransaction();
        _logger.LogInformation("Begin transaction");
        try
        {
            object? result = await next();
            await transaction.CommitAsync(cancellationToken);
            _logger.LogInformation("Commit transaction");
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            await transaction.RollbackAsync(cancellationToken);
            _logger.LogInformation("Rollback transaction");
        }
        return default;
    }
}
