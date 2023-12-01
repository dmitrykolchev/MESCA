// <copyright file="WhiteSpaceCleanerAndAuditingInterceptor.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>


using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata;
using Xobex.Data.Common;

namespace Xobex.Data.EntityFramework.Interceptors;

public class AuditingInterceptor : ISaveChangesInterceptor
{
    private readonly IUser _user;
    private readonly TimeProvider _timeProvider;

    public AuditingInterceptor(IUser user, TimeProvider timeProvider)
    {
        _user = user ?? throw new ArgumentNullException(nameof(user));
        _timeProvider = timeProvider ?? throw new ArgumentNullException(nameof(timeProvider));
    }

    public InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        SetAuditFields(eventData.Context, _timeProvider.GetUtcNow());
        return result;
    }

    public ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        SetAuditFields(eventData.Context, _timeProvider.GetUtcNow());
        return ValueTask.FromResult(result);
    }

    private void SetAuditFields(DbContext? db, DateTimeOffset now)
    {
        if (db is not null)
        {
            IEnumerable<EntityEntry> changedEntries = db.ChangeTracker.Entries();

            foreach (EntityEntry entry in changedEntries)
            {
                if (entry.Entity is IAuditable audit)
                {
                    if (entry.State == EntityState.Added)
                    {
                        ValidateStringProperties(entry);
                        audit.CreatedOn = now;
                        audit.CreatedBy = _user.Id;
                        audit.ModifiedOn = now;
                        audit.ModifiedBy = _user.Id;
                    }
                    else if (entry.State == EntityState.Modified || entry.HasChangedOwnedEntities())
                    {
                        ValidateStringProperties(entry);
                        audit.ModifiedOn = now;
                        audit.ModifiedBy = _user.Id;
                    }
                }
                else if(entry.Entity is ISimpleAuditable simple)
                {
                    if (entry.State == EntityState.Added || 
                        entry.State == EntityState.Modified || entry.HasChangedOwnedEntities())
                    {
                        ValidateStringProperties(entry);
                        simple.ModifiedOn = now;
                        simple.ModifiedBy = _user.Id;
                    }
                }
            }
        }
    }

    private static void ValidateStringProperties(EntityEntry entityEntry)
    {
        if (entityEntry.State is EntityState.Modified or EntityState.Added)
        {
            foreach (IProperty property in entityEntry.CurrentValues.Properties)
            {
                if (property.PropertyInfo?.PropertyType == typeof(string))
                {
                    if (entityEntry.State == EntityState.Modified && property.IsKey())
                    {
                        continue;
                    }
                    string value = entityEntry.CurrentValues.GetValue<string>(property);
                    entityEntry.CurrentValues[property] = string.IsNullOrWhiteSpace(value) ? default
                                                                                           : value.Trim();
                }
            }
        }
    }
}

public static class Extensions
{
    public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
        entry.References.Any(r =>
            r.TargetEntry != null &&
            r.TargetEntry.Metadata.IsOwned() &&
            (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
}
