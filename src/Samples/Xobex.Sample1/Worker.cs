// <copyright file="Worker.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Xobex.Mediator;
using Xobex.Mes.Application;
using Xobex.Mes.Application.Core.DataType;
using Xobex.Mes.Entities.Core;
using Xobex.Mes.Infrastucture.Database;
using Xobex.Sample1.Person;

namespace Xobex.Sample1;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;

    public Worker(IServiceScopeFactory serviceScopeFactory, IHostApplicationLifetime hostLifetime,
        ILogger<Worker> logger)
    {
        ServiceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));
        HostLifetime = hostLifetime;
        _logger = logger;
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

        IMediatorService mediatorService = scope.ServiceProvider.GetRequiredService<IMediatorService>();
        IMesDbContext context = scope.ServiceProvider.GetRequiredService<IMesDbContext>();
        try
        {
            PipelineBuilder<Empty> pipelineBuilder = mediatorService.CreatePipelineBuilder<Empty>();
            Pipeline<Empty> pipeline = pipelineBuilder
                .Use<VerifyInitializedBehavior<DataType>>()
                .Use<TransactedBehavior>()
                .Build();
            await pipeline.RunAsync(InitializeDataTypeCommand.Instance, CancellationToken.None);

            //await mediatorService.SendAsync(InitializeDataTypeCommand.Instance, CancellationToken.None);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }
        await TestMediatorAsync(scope.ServiceProvider, cancellationToken);

        HostLifetime.StopApplication();
    }

    private async Task TestMediatorAsync(IServiceProvider serviceProvider, CancellationToken cancellationToken)
    {
        IMediatorService mediatorService = serviceProvider.GetRequiredService<IMediatorService>();

        CreatePersonCommand personCommand = new()
        {
            FirstName = "Dmitry",
            LastName = "Kolchev"
        };

        int result = await mediatorService.QueryAsync(personCommand, cancellationToken);
        personCommand = new CreatePersonCommand
        {
            FirstName = "Ivan",
            LastName = "Pupkin"
        };
        result = await mediatorService.QueryAsync(personCommand, cancellationToken);

        try
        {
            personCommand = new CreatePersonCommand
            {
                LastName = "Petrov"
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
