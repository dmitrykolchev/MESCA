// <copyright file="DbContextBase.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Storage;

namespace Xobex.Data.EntityFramework;

public abstract class DbContextBase : DbContext, ITransactionProvider
{
    protected DbContextBase(DbContextOptions options) : base(options)
    {
    }

    public ITransactionWrapper BeginTransaction()
    {
        if (Database.CurrentTransaction is not null)
        {
            return InnerTransaction.Instance;
        }
        return new TransactionWrapper(Database.BeginTransaction());
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Conventions.Remove(typeof(ForeignKeyIndexConvention));
        base.ConfigureConventions(configurationBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var entityWithAttributes = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(t => t.PropertyType.IsGenericType
                     && t.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>)
                     && t.GetCustomAttribute<EntityConfigurationAttribute>() != null)
            .Select(t => new
            {
                EntityType = t.PropertyType.GetGenericArguments()[0],
                Attribute = t.GetCustomAttribute<EntityConfigurationAttribute>()
            })
            .OrderBy(t => t.EntityType.Namespace).ThenBy(t => t.EntityType.Name);
        foreach (var item in entityWithAttributes)
        {
            IModelConfiguration configuration = (IModelConfiguration)Activator.CreateInstance(item.Attribute!.EntityConfigurationType)!;
            configuration.Configure(modelBuilder, this);
        }

        IEnumerable<EntityConfigurationAttribute> attributes = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(t => t.PropertyType.IsGenericType && t.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))
            .Select(t => t.PropertyType.GetGenericArguments()[0])
            .OrderBy(t => t.Namespace).ThenBy(t => t.Name)
            .Select(t => t.GetCustomAttribute<EntityConfigurationAttribute>())
            .Where(t => t != null)!;
        foreach (EntityConfigurationAttribute attr in attributes)
        {
            IModelConfiguration configuration = (IModelConfiguration)Activator.CreateInstance(attr.EntityConfigurationType)!;
            configuration.Configure(modelBuilder, this);
        }
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
