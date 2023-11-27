// <copyright file="TransactionProvider.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Xobex.Mes.Application;

public interface ITransactionProvider
{
    ITransactionWrapper BeginTransaction();
}

internal class TransactionProvider: ITransactionProvider
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
