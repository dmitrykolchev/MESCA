// <copyright file="InitializeDocumentTypesCommandHandler.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xobex.Entities.Metadata;
using Xobex.Mediator;
using Xobex.Mes.Entities.Resources;

namespace Xobex.Mes.Application.Metadata.DocumentTypes;

[MediatorLifetime(ServiceLifetime.Scoped)]
public class InitializeDocumentTypesCommandHandler : DatabaseRequestHandler<InitializeDocumentTypesCommand, Empty>
{
    public InitializeDocumentTypesCommandHandler(IMesDbContext db,
        ILogger<InitializeDocumentTypesCommandHandler> logger) : base(db, logger)
    {
    }

    protected override async Task<Empty> ProcessOverrideAsync(InitializeDocumentTypesCommand request, CancellationToken cancellationToken)
    {
        DocumentType equipment = new()
        {
            State = DocumentTypeState.Active,
            Flags = DocumentTypeFlags.None,
            Code = "Equipment",
            Name = "Оборудование",
            Image = "Equipment",
            Description = "Справочник оборудования"
        };
        equipment.States = new HashSet<DocumentState>([
            new ()
            {
                State = DocumentStateState.Active,
                Value = (short)EquipmentState.NotExists,
                Code = nameof(EquipmentState.NotExists),
                Name = "Не существует",
            },
            new ()
            {
                State = DocumentStateState.Active,
                Value = (short)EquipmentState.Active,
                Code = nameof(EquipmentState.Active),
                Name = "Используется",
            },
            new ()
            {
                State = DocumentStateState.Active,
                Value = (short)EquipmentState.Inactive,
                Code = nameof(EquipmentState.Inactive),
                Name = "Не используется",
            }
        ]);
        Db.DocumentType.Add(equipment);
        await Db.SaveChangesAsync(cancellationToken);
        return Empty.Instance;
    }
}
