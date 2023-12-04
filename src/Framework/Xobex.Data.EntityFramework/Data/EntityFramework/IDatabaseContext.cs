// <copyright file="ITransactionProvider.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.EntityFrameworkCore;

namespace Xobex.Data.EntityFramework;

public interface IDatabaseContext
{
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
    ITransactionWrapper BeginTransaction();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}

