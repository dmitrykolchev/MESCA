// <copyright file="InitializeDataTypeCommandHandler.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xobex.Mediator;
using Xobex.Entities.Metadata;
using Xobex.Mes.Entities;
using Xobex.Entities;

namespace Xobex.Mes.Application.Core.DataTypes;

[MediatorLifetime(ServiceLifetime.Scoped)]
public class InitializeDataTypesCommandHandler : DatabaseRequestHandler<InitializeDataTypesCommand, Empty>
{
    public InitializeDataTypesCommandHandler(
        IMesDbContext db,
        ILogger<InitializeDataTypesCommandHandler> logger): base(db, logger)
    {
    }

    protected override async Task<Empty> HandleOverrideAsync(InitializeDataTypesCommand request, CancellationToken cancellation)
    {
        Db.DataType.AddRange(
            [
                new()
                {
                    Id = (int)DataKind.Int,
                    State = CommonStates.Active,
                    Code = nameof(DataKind.Int),
                    Name = "Целое число",
                    Kind = DataKind.Int
                },
                new()
                {
                    Id = (int)DataKind.Boolean,
                    State = CommonStates.Active,
                    Code = nameof(DataKind.Boolean),
                    Name = "Логический (да/нет)",
                    Kind = DataKind.Boolean
                },
                new()
                {
                    Id = (int)DataKind.DateTime,
                    State = CommonStates.Active,
                    Code = nameof(DataKind.DateTime),
                    Name = "Дата/время",
                    Kind = DataKind.DateTime
                },
                new()
                {
                    Id = (int)DataKind.String,
                    State = CommonStates.Active,
                    Code = nameof(DataKind.String),
                    Name = "Текст",
                    Kind = DataKind.String
                },
                new()
                {
                    Id = (int)DataKind.Decimal,
                    State = CommonStates.Active,
                    Code = nameof(DataKind.Decimal),
                    Name = "Число с фиксированной десятичной точкой",
                    Kind = DataKind.Decimal
                },
                new()
                {
                    Id = (int)DataKind.Double,
                    State = CommonStates.Active,
                    Code = nameof(DataKind.Double),
                    Name = "Число с плавающей десятичной точкой",
                    Kind = DataKind.Double
                },
                new()
                {
                    Id = (int)DataKind.Binary,
                    State = CommonStates.Active,
                    Code = nameof(DataKind.Binary),
                    Name = "Двоичные данные",
                    Kind = DataKind.Binary
                },
                new()
                {
                    Id = (int)DataKind.DocumentReference,
                    State = CommonStates.Active,
                    Code = nameof(DataKind.DocumentReference),
                    Name = "Ссылка на документ",
                    Kind = DataKind.DocumentReference
                },
                new()
                {
                    Id = (int)DataKind.Complex,
                    State = CommonStates.Active,
                    Code = nameof(DataKind.Complex),
                    Name = "Составной тип",
                    Kind = DataKind.Complex
                }
            ]);

        await Db.SaveChangesAsync(cancellation);

        Logger.LogInformation("{Command} executed successfully", nameof(InitializeDataTypesCommand));
        return Empty.Instance;
    }
}
