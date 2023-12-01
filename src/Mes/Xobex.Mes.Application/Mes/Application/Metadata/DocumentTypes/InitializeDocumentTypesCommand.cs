// <copyright file="InitializeDocumentTypesCommand.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Xobex.Data.Enums;
using Xobex.Mediator;

namespace Xobex.Mes.Application.Metadata.DocumentTypes;

public class InitializeDocumentTypesCommand : MesRequest<Empty>
{
    public static readonly InitializeDocumentTypesCommand Instance = new();

    private InitializeDocumentTypesCommand() { }

    public override RequestVerbs Verb => RequestVerbs.Process;
}
