// <copyright file="PipelineBuilder.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.Extensions.DependencyInjection;

namespace Xobex.Mediator;

public class PipelineBuilder<TResult>
{
    private readonly Stack<IBehavior<TResult>> _behaviors = new();

    public PipelineBuilder(IMediatorService mediatorService, IServiceProvider serviceProvider)
    {
        MediatorService = mediatorService ?? throw new ArgumentNullException(nameof(mediatorService));
        ServiceProvider = serviceProvider;
    }

    public IMediatorService MediatorService { get; }
    public IServiceProvider ServiceProvider { get; }

    public PipelineBuilder<TResult> Use<TBehavior>()
        where TBehavior : IBehavior<TResult>
    {
        return Use(ActivatorUtilities.CreateInstance<TBehavior>(ServiceProvider));
    }

    public PipelineBuilder<TResult> Use(IBehavior<TResult> behavior)
    {
        ArgumentNullException.ThrowIfNull(behavior);
        _behaviors.Push(behavior);
        return this;
    }

    public Pipeline<TResult> Build()
    {
        Pipeline<TResult> pipeline = new();
        pipeline.Root = new(pipeline, MediatorService.QueryAsync);
        while (_behaviors.Count > 0)
        {
            IBehavior<TResult> behavior = _behaviors.Pop();
            Pipeline<TResult>.PipelineEntry temp = new(pipeline, behavior);
            temp.Next = pipeline.Root;
            pipeline.Root = temp;
        }
        return pipeline;
    }
}
