// <copyright file="InitializedDataTypeCommandValidator.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xobex.Mes.Entities;
using Xobex.Mediator;

namespace Xobex.Mes.Application.Core.DataTypes;

[MediatorLifetime(ServiceLifetime.Scoped)]
public class InitializedDataTypesCommandValidator : Validator<InitializeDataTypesCommand>
{
    public InitializedDataTypesCommandValidator(IMesDbContext db)
    {
        Db = db ?? throw new ArgumentNullException(nameof(db));
    }

    public IMesDbContext Db { get; }

    public async override Task ValidateAsync(InitializeDataTypesCommand request, CancellationToken cancellationToken)
    {
        if (await Db.DataType.AnyAsync(cancellationToken))
        {
            throw new InvalidOperationException("DataType already initialized");
        }
    }
}
