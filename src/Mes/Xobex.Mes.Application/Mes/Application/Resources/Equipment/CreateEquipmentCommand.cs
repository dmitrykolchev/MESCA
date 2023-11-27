// <copyright file="CreateEquipmentCommand.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xobex.Mediator;

namespace Xobex.Mes.Application.Resources.Equipment;

public class CreateEquipmentCommand : IRequest<int>
{
    public required string Code { get; set; }
    public required string Name { get; set; }
    public string? Comments { get; set; }
}
