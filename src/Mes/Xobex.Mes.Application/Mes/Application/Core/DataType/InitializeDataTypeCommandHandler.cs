// <copyright file="InitializeDataTypeCommandHandler.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xobex.Mediator;
using Xobex.Mes.Entities.Core;

namespace Xobex.Mes.Application.Core.DataType;

[MediatorLifetime(ServiceLifetime.Scoped)]
public class InitializeDataTypeCommandHandler : RequestHandler<InitializeDataTypeCommand>
{
    public InitializeDataTypeCommandHandler(
        IMesDbContext db,
        ILogger<InitializeDataTypeCommandHandler> logger)
    {
        Db = db ?? throw new ArgumentNullException(nameof(db));
        Logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    private IMesDbContext Db { get; }

    private ILogger Logger { get; }

    public override async Task ProcessAsync(InitializeDataTypeCommand request, CancellationToken cancellation)
    {
        if (await Db.DataType.AnyAsync(cancellation))
        {
            return;
        }

        using var transaction = ((DbContext)Db).Database.BeginTransaction();

        Db.DataType.AddRange(
            [
                new()
                {
                    Id = (int)DataKind.Int,
                    State = DataTypeState.Active,
                    Code = nameof(DataKind.Int),
                    Name = "Целое число",
                    Kind = DataKind.Int
                },
                new()
                {
                    Id = (int)DataKind.Boolean,
                    State = DataTypeState.Active,
                    Code = nameof(DataKind.Boolean),
                    Name = "Лигический (да/нет)",
                    Kind = DataKind.Boolean
                },
                new()
                {
                    Id = (int)DataKind.DateTime,
                    State = DataTypeState.Active,
                    Code = nameof(DataKind.DateTime),
                    Name = "Дата/время",
                    Kind = DataKind.DateTime
                },
                new()
                {
                    Id = (int)DataKind.String,
                    State = DataTypeState.Active,
                    Code = nameof(DataKind.String),
                    Name = "Текст",
                    Kind = DataKind.String
                },
                new()
                {
                    Id = (int)DataKind.Decimal,
                    State = DataTypeState.Active,
                    Code = nameof(DataKind.Decimal),
                    Name = "Число с фиксированной десятичной точкой",
                    Kind = DataKind.Decimal
                },
                new()
                {
                    Id = (int)DataKind.Double,
                    State = DataTypeState.Active,
                    Code = nameof(DataKind.Double),
                    Name = "Число с плавающей десятичной точкой",
                    Kind = DataKind.Double
                },
                new()
                {
                    Id = (int)DataKind.Binary,
                    State = DataTypeState.Active,
                    Code = nameof(DataKind.Binary),
                    Name = "Двоичные данные",
                    Kind = DataKind.Binary
                },
                new()
                {
                    Id = (int)DataKind.DocumentReference,
                    State = DataTypeState.Active,
                    Code = nameof(DataKind.DocumentReference),
                    Name = "Ссылка на документ",
                    Kind = DataKind.DocumentReference
                },
                new()
                {
                    Id = (int)DataKind.Complex,
                    State = DataTypeState.Active,
                    Code = nameof(DataKind.Complex),
                    Name = "Составной тип",
                    Kind = DataKind.Complex
                }
            ]);

        await Db.SaveChangesAsync(cancellation);
        await transaction.CommitAsync(cancellation);

        Logger.LogInformation("{Command} executed successfully", nameof(InitializeDataTypeCommand));
    }
}
