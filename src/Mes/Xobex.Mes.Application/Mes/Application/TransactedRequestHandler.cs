// <copyright file="TransactedRequestHandler.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Xobex.Mediator;

namespace Xobex.Mes.Application;

public abstract class TransactedRequestHandler<TRequest, TResponse> : RequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly TransactionProvider _transactionProvider;

    protected TransactedRequestHandler(IMesDbContext db, ILogger logger)
    {
        _transactionProvider = new((DbContext)db);
        Db = db ?? throw new ArgumentNullException(nameof(db));
        Logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    protected IMesDbContext Db { get; }

    protected ILogger Logger { get; }

    public sealed override async Task<TResponse> ProcessAsync(TRequest request, CancellationToken cancellationToken)
    {
        using var transaction = _transactionProvider.BeginTransaction();
        try
        {
            TResponse result = await ProcessOverrideAsync(request, cancellationToken).ConfigureAwait(false);
            await transaction.CommitAsync(cancellationToken).ConfigureAwait(false);
            return result;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            await transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
            throw;
        }
    }
}

internal class TransactionProvider
{
    private readonly DbContext _context;

    public TransactionProvider(DbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public ITransactionWrapper BeginTransaction()
    {
        if (_context.Database.CurrentTransaction is not null)
        {
            return InnerTransaction.Instance;
        }
        return new TransactionWrapper(_context.Database.BeginTransaction());
    }

    private class TransactionWrapper : ITransactionWrapper
    {
        private readonly IDbContextTransaction _contextTransaction;

        public TransactionWrapper(IDbContextTransaction contextTransaction)
        {
            _contextTransaction = contextTransaction ?? throw new ArgumentNullException(nameof(contextTransaction));
        }

        public Task CommitAsync(CancellationToken cancellationToken)
        {
            return _contextTransaction.CommitAsync();
        }

        public void Dispose()
        {
            _contextTransaction.Dispose();
        }

        public Task RollbackAsync(CancellationToken cancellationToken)
        {
            return _contextTransaction.RollbackAsync();
        }
    }

    private class InnerTransaction : ITransactionWrapper
    {
        public readonly static InnerTransaction Instance = new();

        private InnerTransaction()
        {
        }

        Task ITransactionWrapper.CommitAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
        Task ITransactionWrapper.RollbackAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        void IDisposable.Dispose()
        {
        }
    }
}
