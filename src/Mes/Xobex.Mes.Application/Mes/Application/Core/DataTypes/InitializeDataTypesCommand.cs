// <copyright file="InitializeDataTypeCommand.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Xobex.Domain.Enums;
using Xobex.Mediator;

namespace Xobex.Mes.Application.Core.DataTypes;

public class InitializeDataTypesCommand : MesRequest<Empty>
{
    public static readonly InitializeDataTypesCommand Instance = new ();

    private InitializeDataTypesCommand() { }

    public override RequestVerbs Verb => RequestVerbs.Process;
}
