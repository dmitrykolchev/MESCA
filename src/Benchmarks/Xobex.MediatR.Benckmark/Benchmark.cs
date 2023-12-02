// <copyright file="Benchmark.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using BenchmarkDotNet.Attributes;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Xobex.Mediator.Benchmark.Person;
using Xobex.MediatR.Benchmark.Person;

namespace Xobex.MediatR.Benckmark;

public class Benchmark
{
    private IServiceProvider _serviceProvider = null!;
    private IServiceScopeFactory _serviceScopeFactory = null!;

    [GlobalSetup]    
    public void Initialize()
    {
        ServiceCollection services = new ();
        services.AddMediatR((config) =>
        {
            config.Lifetime = ServiceLifetime.Singleton;
            config.RegisterServicesFromAssembly(typeof(AddPersonCommand).Assembly);
        });
        services.AddScoped<PersonRepository>();

        _serviceProvider = services.BuildServiceProvider();
        _serviceScopeFactory = _serviceProvider.GetRequiredService<IServiceScopeFactory>();
    }

    [Benchmark]
    public async Task TestMediatorAsync()
    {
        CancellationToken cancellationToken = CancellationToken.None;
        using IServiceScope scope = _serviceScopeFactory.CreateScope();

        IMediator mediatorService = scope.ServiceProvider.GetRequiredService<IMediator>();

        AddPersonCommand personCommand = new()
        {
            FirstName = "Dmitry",
            LastName = "Kolchev"
        };

        int result = await mediatorService.Send(personCommand, cancellationToken);
        personCommand = new AddPersonCommand
        {
            FirstName = "Ivan",
            LastName = "Pupkin"
        };
        result = await mediatorService.Send(personCommand, cancellationToken);

        PersonModel person = await mediatorService.Send(
            new GetPersonCommand(1),
            cancellationToken);

        IEnumerable<PersonModel> people = await mediatorService.Send(
            GetPeopleCommand.Instance, cancellationToken);
    }
}
