// <copyright file="EntityConfiguration`1.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using System.Linq.Expressions;
using System.Reflection;
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
    protected EntityConfiguration(bool hasSerialId = true)
    {
        TableName = typeof(TEntity).Name.ToSnakeCase();
        string ns = typeof(TEntity).Namespace!.Split('.')[^1];
        SchemaName = GetType().Assembly
            .GetCustomAttributes<NamespaceMappingAttribute>()
            .Where(t => t.Namespace == ns)
            .Select(t => t.SchemaName)
            .FirstOrDefault(ns)
            .ToSnakeCase();
        HasSerialId = hasSerialId;
    }

    private EntityConfiguration(string tableName, string schemaName)
    {
        TableName = tableName ?? throw new ArgumentNullException(nameof(tableName));
        SchemaName = schemaName ?? throw new ArgumentNullException(nameof(schemaName));
    }

    protected string TableName { get; }

    protected string SchemaName { get; }

    protected bool HasSerialId { get; }

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

    protected string GetDefaultSequenceValue()
    {
        return DatabaseType switch
        {
            DatabaseType.SqlServer => $"next value for [{SchemaName}].[{TableName}_seq]",
            DatabaseType.PostgreSql => $"nextval('{SchemaName}.{TableName}_seq'::regclass)",
            _ => throw new InvalidOperationException($"unsupported database type: {DatabaseType}")
        };
    }

    protected virtual void OnConfigureModel(ModelBuilder modelBuilder)
    {
        if (HasSerialId)
        {
            modelBuilder.HasSequence<long>(TableName + "_seq", SchemaName);
        }
    }

    protected abstract void OnConfigureEntity(EntityTypeBuilder<TEntity> entity);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="entity"></param>
    /// <param name="idExpression"></param>
    /// <param name="keyExpression"></param>
    protected void ToTableWithKey<TKey>(
        EntityTypeBuilder<TEntity> entity,
        Expression<Func<TEntity, TKey>> idExpression,
        Expression<Func<TEntity, object?>> keyExpression)
    {
        entity.ToTable(TableName, SchemaName);
        if (HasSerialId)
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

