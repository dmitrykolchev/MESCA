// <copyright file="InitializeDataTypesPostProcessor.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.Extensions.Logging;
using Xobex.Mediator;

namespace Xobex.Mes.Application.Core.DataTypes;

public class InitializeDataTypesPostProcessor : RequestPostProcessor<InitializeDataTypesCommand, Empty>
{
    public InitializeDataTypesPostProcessor(ILogger<InitializeDataTypesPostProcessor> logger)
    {
        Logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public ILogger Logger { get; }

    public override Task ProcessAsync(InitializeDataTypesCommand request, Empty response, CancellationToken cancellationToken)
    {
        Logger.LogInformation("InitializeDataType request post processor");
        return Task.CompletedTask;
    }
}
