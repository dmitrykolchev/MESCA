// <copyright file="AddEquipmentCommand.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Xobex.Data.Enums;
using Xobex.Mediator;

namespace Xobex.Mes.Application.Resources.Equipment;

public class AddEquipmentCommand : MesRequest<int>
{
    public override RequestVerbs Verb => RequestVerbs.Add;
    public required string Code { get; set; }
    public required string Name { get; set; }
    public string? Comments { get; set; }
}
