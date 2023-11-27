// <copyright file="TransactedBehavior.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Xobex.Mediator;

namespace Xobex.Mes.Application;

public class TransactedBehavior<TResult> : Behavior<TResult>
{
    private readonly ITransactionProvider _transactionProvider;
    private readonly ILogger _logger;

    public TransactedBehavior(IMesDbContext context, ILogger<TransactedBehavior<TResult>> logger)
    {
        _transactionProvider = new TransactionProvider((DbContext)context);
        _logger = logger;
    }

    public override async Task<TResult?> ProcessAsync(IRequest<TResult> request, Func<Task<TResult>>? next, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(next);

        using ITransactionWrapper transaction = _transactionProvider.BeginTransaction();
        _logger.LogInformation("Begin transaction");
        try
        {
            TResult? result = (await next());
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
