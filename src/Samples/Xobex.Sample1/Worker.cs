// <copyright file="Worker.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Xobex.Data.Entities.Metadata;
using Xobex.Mediator;
using Xobex.Data.Mes.Application;
using Xobex.Data.Mes.Application.Core.DataTypes;
using Xobex.Data.Mes.Application.Metadata.DocumentTypes;
using Xobex.Data.Mes.Infrastucture.Database;
using Xobex.Data.Sample1.Person;

namespace Xobex.Data.Sample1;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;

    public Worker(IServiceScopeFactory serviceScopeFactory, IHostApplicationLifetime hostLifetime,
        ILogger<Worker> logger)
    {
        ServiceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));
        HostLifetime = hostLifetime ?? throw new ArgumentNullException(nameof(hostLifetime));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    private IServiceScopeFactory ServiceScopeFactory { get; }
    private IHostApplicationLifetime HostLifetime { get; }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        return base.StartAsync(cancellationToken);
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        return base.StopAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        using IServiceScope scope = ServiceScopeFactory.CreateScope();
        MesSqlServerDbContext db = scope.ServiceProvider.GetRequiredService<MesSqlServerDbContext>();
        db.Database.EnsureCreated();

        IMediator mediatorService = scope.ServiceProvider.GetRequiredService<IMediator>();
        IMesDbContext context = scope.ServiceProvider.GetRequiredService<IMesDbContext>();
        try
        {
            PipelineBuilder pipelineBuilder = mediatorService.CreatePipelineBuilder();
            Pipeline pipeline = pipelineBuilder
                .Use<VerifyInitializedBehavior<DataType>>()
                .Use<TransactedBehavior>()
                .Build();
            _ = await pipeline.RunAsync(InitializeDataTypesCommand.Instance, CancellationToken.None);

            pipelineBuilder = mediatorService.CreatePipelineBuilder();
            pipeline = pipelineBuilder
                .Use<VerifyInitializedBehavior<DocumentType>>()
                .Use<TransactedBehavior>()
                .Build();
            _ = await pipeline.RunAsync(InitializeDocumentTypesCommand.Instance, CancellationToken.None);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }
        //await TestMediatorAsync(scope.ServiceProvider, cancellationToken);

        HostLifetime.StopApplication();
    }

    private async Task TestMediatorAsync(IServiceProvider serviceProvider, CancellationToken cancellationToken)
    {
        IMediator mediatorService = serviceProvider.GetRequiredService<IMediator>();

        AddPersonCommand personCommand = new()
        {
            FirstName = "Dmitry",
            LastName = "Kolchev"
        };

        int result = await mediatorService.QueryAsync(personCommand, cancellationToken);
        personCommand = new AddPersonCommand
        {
            FirstName = "Ivan",
            LastName = "Pupkin"
        };
        result = await mediatorService.QueryAsync(personCommand, cancellationToken);

        try
        {
            personCommand = new AddPersonCommand
            {
                LastName = "Petrov",
                FirstName = null!
            };
            result = await mediatorService.QueryAsync(personCommand, cancellationToken);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        PersonModel person = await mediatorService.QueryAsync(
            new GetPersonCommand(1),
            cancellationToken);

        IEnumerable<PersonModel> people = await mediatorService.QueryAsync(
            GetPeopleCommand.Instance, cancellationToken);

        foreach (PersonModel item in people)
        {
            Console.WriteLine($"{item.Id}: {item.LastName}, {item.FirstName}");
        }
    }

}
