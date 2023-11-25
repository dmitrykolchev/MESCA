// <copyright file="Worker.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xobex.Mediator;
using Xobex.Mes.Application;
using Xobex.Mes.Application.Core.DataType;
using Xobex.Mes.Infrastucture.Database;
using Xobex.Sample1.Person;

namespace Xobex.Sample1;
public class Worker : IHostedService
{
    public Worker(IServiceScopeFactory serviceScopeFactory)
    {
        ServiceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));
    }

    private IServiceScopeFactory ServiceScopeFactory { get; }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using IServiceScope scope = ServiceScopeFactory.CreateScope();
        MesSqlServerDbContext db = scope.ServiceProvider.GetRequiredService<MesSqlServerDbContext>();
        db.Database.EnsureCreated();

        IMediatorService mediatorService = scope.ServiceProvider.GetRequiredService<IMediatorService>();
        IMesDbContext context = scope.ServiceProvider.GetRequiredService<IMesDbContext>();
        await mediatorService.SendAsync(new InitializeDataTypeCommand(), CancellationToken.None);
        await mediatorService.SendAsync(new InitializeDataTypeCommand(), CancellationToken.None);
        await TestMediatorAsync(scope.ServiceProvider, cancellationToken);
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

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

}
