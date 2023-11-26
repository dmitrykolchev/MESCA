// <copyright file="InitializedDataTypeCommandValidator.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xobex.Mediator;

namespace Xobex.Mes.Application.Core.DataType;

[MediatorLifetime(ServiceLifetime.Scoped)]
public class InitializedDataTypeCommandValidator : Validator<InitializeDataTypeCommand>
{
    public InitializedDataTypeCommandValidator(IMesDbContext db)
    {
        Db = db ?? throw new ArgumentNullException(nameof(db));
    }

    public IMesDbContext Db { get; }

    public async override Task ValidateAsync(InitializeDataTypeCommand request, CancellationToken cancellationToken)
    {
        if (await Db.DataType.AnyAsync(cancellationToken))
        {
            throw new InvalidOperationException("DataType already initialized");
        }
    }
}
