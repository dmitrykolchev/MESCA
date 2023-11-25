using System.Globalization;
using EFCore.NamingConventions.Internal;
using Microsoft.EntityFrameworkCore.Infrastructure;

// ReSharper disable once CheckNamespace
namespace Microsoft.EntityFrameworkCore;

public static class NamingConventionsExtensions
{
    public static DbContextOptionsBuilder UseSnakeCaseNamingConvention(
        this DbContextOptionsBuilder optionsBuilder,
        CultureInfo? culture = null)
    {
        Check.NotNull(optionsBuilder, nameof(optionsBuilder));

        NamingConventionsOptionsExtension extension = (optionsBuilder.Options.FindExtension<NamingConventionsOptionsExtension>()
                ?? new NamingConventionsOptionsExtension())
            .WithSnakeCaseNamingConvention(culture);

        ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(extension);

        return optionsBuilder;
    }

    public static DbContextOptionsBuilder<TContext> UseSnakeCaseNamingConvention<TContext>(
        this DbContextOptionsBuilder<TContext> optionsBuilder, CultureInfo? culture = null)
        where TContext : DbContext
    {
        return (DbContextOptionsBuilder<TContext>)UseSnakeCaseNamingConvention((DbContextOptionsBuilder)optionsBuilder, culture);
    }

    public static DbContextOptionsBuilder UseLowerCaseNamingConvention(
        this DbContextOptionsBuilder optionsBuilder,
        CultureInfo? culture = null)
    {
        Check.NotNull(optionsBuilder, nameof(optionsBuilder));

        NamingConventionsOptionsExtension extension = (optionsBuilder.Options.FindExtension<NamingConventionsOptionsExtension>()
                ?? new NamingConventionsOptionsExtension())
            .WithLowerCaseNamingConvention(culture);

        ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(extension);

        return optionsBuilder;
    }

    public static DbContextOptionsBuilder<TContext> UseLowerCaseNamingConvention<TContext>(
        this DbContextOptionsBuilder<TContext> optionsBuilder,
        CultureInfo? culture = null)
        where TContext : DbContext
    {
        return (DbContextOptionsBuilder<TContext>)UseLowerCaseNamingConvention((DbContextOptionsBuilder)optionsBuilder, culture);
    }

    public static DbContextOptionsBuilder UseUpperCaseNamingConvention(
        this DbContextOptionsBuilder optionsBuilder,
        CultureInfo? culture = null)
    {
        Check.NotNull(optionsBuilder, nameof(optionsBuilder));

        NamingConventionsOptionsExtension extension = (optionsBuilder.Options.FindExtension<NamingConventionsOptionsExtension>()
                ?? new NamingConventionsOptionsExtension())
            .WithUpperCaseNamingConvention(culture);

        ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(extension);

        return optionsBuilder;
    }

    public static DbContextOptionsBuilder<TContext> UseUpperCaseNamingConvention<TContext>(
        this DbContextOptionsBuilder<TContext> optionsBuilder,
        CultureInfo? culture = null)
        where TContext : DbContext
    {
        return (DbContextOptionsBuilder<TContext>)UseUpperCaseNamingConvention((DbContextOptionsBuilder)optionsBuilder, culture);
    }

    public static DbContextOptionsBuilder UseUpperSnakeCaseNamingConvention(
        this DbContextOptionsBuilder optionsBuilder,
        CultureInfo? culture = null)
    {
        Check.NotNull(optionsBuilder, nameof(optionsBuilder));

        NamingConventionsOptionsExtension extension = (optionsBuilder.Options.FindExtension<NamingConventionsOptionsExtension>()
                ?? new NamingConventionsOptionsExtension())
            .WithUpperSnakeCaseNamingConvention(culture);

        ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(extension);

        return optionsBuilder;
    }

    public static DbContextOptionsBuilder<TContext> UseUpperSnakeCaseNamingConvention<TContext>(
        this DbContextOptionsBuilder<TContext> optionsBuilder,
        CultureInfo? culture = null)
        where TContext : DbContext
    {
        return (DbContextOptionsBuilder<TContext>)UseUpperSnakeCaseNamingConvention((DbContextOptionsBuilder)optionsBuilder, culture);
    }

    public static DbContextOptionsBuilder UseCamelCaseNamingConvention(
        this DbContextOptionsBuilder optionsBuilder,
        CultureInfo? culture = null)
    {
        Check.NotNull(optionsBuilder, nameof(optionsBuilder));

        NamingConventionsOptionsExtension extension = (optionsBuilder.Options.FindExtension<NamingConventionsOptionsExtension>()
                ?? new NamingConventionsOptionsExtension())
            .WithCamelCaseNamingConvention(culture);

        ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(extension);

        return optionsBuilder;
    }

    public static DbContextOptionsBuilder<TContext> UseCamelCaseNamingConvention<TContext>(
        this DbContextOptionsBuilder<TContext> optionsBuilder,
        CultureInfo? culture = null)
        where TContext : DbContext
    {
        return (DbContextOptionsBuilder<TContext>)UseCamelCaseNamingConvention((DbContextOptionsBuilder)optionsBuilder, culture);
    }
}
