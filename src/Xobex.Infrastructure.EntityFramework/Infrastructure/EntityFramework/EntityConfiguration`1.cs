// <copyright file="EntityConfiguration`1.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using EFCore.NamingConventions.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Xobex.Infrastructure.EntityFramework;

public enum DatabaseType
{
    Unknown,
    SqlServer,
    PostgreSql
}

public abstract class EntityConfiguration<TEntity> : IModelConfiguration
    where TEntity : class
{
    private static readonly SnakeCaseNameRewriter s_snakeCaseRewriter = new(CultureInfo.InvariantCulture);

    protected EntityConfiguration(bool hasIdentity = true)
    {
        TableName = s_snakeCaseRewriter.RewriteName(typeof(TEntity).Name);
        string ns = typeof(TEntity).Namespace!.Split('.')[^1];
        SchemaName = s_snakeCaseRewriter.RewriteName(GetType().Assembly
            .GetCustomAttributes<NamespaceMappingAttribute>()
            .Where(t => t.Namespace == ns)
            .Select(t => t.SchemaName)
            .FirstOrDefault(ns));
        HasIdentity = hasIdentity;
    }

    protected string TableName { get; }

    protected string SchemaName { get; }

    protected bool HasIdentity { get; }

    public void Configure(ModelBuilder modelBuilder, DbContext context)
    {
        DatabaseType = context switch
        {
            ISqlServer => DatabaseType.SqlServer,
            IPostgreSql => DatabaseType.PostgreSql,
            _ => throw new InvalidOperationException("unsupported database type")
        };
        OnConfigureModel(modelBuilder);
        modelBuilder.Entity<TEntity>(OnConfigureEntity);
    }

    protected DatabaseType DatabaseType { get; private set; }
    /// <summary>
    /// Returns default value expression for identity column
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    protected string GetDefaultSequenceValue()
    {
        return DatabaseType switch
        {
            DatabaseType.SqlServer => $"next value for [{SchemaName}].[{TableName}_seq]",
            DatabaseType.PostgreSql => $"nextval('{SchemaName}.{TableName}_seq'::regclass)",
            _ => throw new InvalidOperationException($"unsupported database type: {DatabaseType}")
        };
    }
    /// <summary>
    /// Configures model for the entity. Default implementation adds sequence for identity columns
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected virtual void OnConfigureModel(ModelBuilder modelBuilder)
    {
        if (HasIdentity)
        {
            modelBuilder.HasSequence<long>(TableName + "_seq", SchemaName);
        }
    }

    protected abstract void OnConfigureEntity(EntityTypeBuilder<TEntity> entity);
    /// <summary>
    /// Configures the table with or without identity and with primary key
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="entity">Entity builder instance</param>
    /// <param name="idExpression">identity field expression</param>
    /// <param name="keyExpression">primary key expression</param>
    protected void ToTableWithKey<TKey>(
        EntityTypeBuilder<TEntity> entity,
        Expression<Func<TEntity, TKey>> idExpression,
        Expression<Func<TEntity, object?>> keyExpression)
    {
        entity.ToTable(TableName, SchemaName);
        if (HasIdentity)
        {
            entity.Property(idExpression).HasDefaultValueSql(GetDefaultSequenceValue());
        }
        else
        {
            entity.Property(idExpression).ValueGeneratedNever();
        }
        entity.HasKey(keyExpression).HasName($"pk_{TableName}");
    }
}

