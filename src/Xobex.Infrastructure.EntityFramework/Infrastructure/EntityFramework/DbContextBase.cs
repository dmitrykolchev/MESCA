// <copyright file="DbContextBase.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Xobex.Infrastructure.EntityFramework;

public abstract class DbContextBase : DbContext
{
    protected DbContextBase(DbContextOptions options) : base(options)
    {
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
}
