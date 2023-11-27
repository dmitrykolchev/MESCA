// <copyright file="PipelineBuilder.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.Extensions.DependencyInjection;

namespace Xobex.Mediator;

public class PipelineBuilder<TResult>
{
    private readonly Stack<IBehavior> _behaviors = new();

    internal PipelineBuilder(IMediatorService mediatorService, IServiceProvider serviceProvider)
    {
        MediatorService = mediatorService ?? throw new ArgumentNullException(nameof(mediatorService));
        ServiceProvider = serviceProvider;
    }

    public IMediatorService MediatorService { get; }
    public IServiceProvider ServiceProvider { get; }

    /// <summary>
    /// Creates instance of TBehavior and adds to the pipeline
    /// </summary>
    /// <typeparam name="TBehavior"></typeparam>
    /// <returns></returns>
    public PipelineBuilder<TResult> Use<TBehavior>()
        where TBehavior : IBehavior
    {
        return Use(ActivatorUtilities.CreateInstance<TBehavior>(ServiceProvider));
    }
    /// <summary>
    /// Adds behavior instance to the pipeline
    /// </summary>
    /// <param name="behavior"></param>
    /// <returns></returns>
    public PipelineBuilder<TResult> Use(IBehavior behavior)
    {
        ArgumentNullException.ThrowIfNull(behavior);
        _behaviors.Push(behavior);
        return this;
    }
    /// <summary>
    /// Builds pipeline
    /// </summary>
    /// <returns></returns>
    public Pipeline<TResult> Build()
    {
        Pipeline<TResult> pipeline = new();
        pipeline.Root = new(pipeline, MediatorService.QueryAsync);
        while (_behaviors.Count > 0)
        {
            IBehavior behavior = _behaviors.Pop();
            Pipeline<TResult>.PipelineEntry temp = new(pipeline, behavior)
            {
                Next = pipeline.Root
            };
            pipeline.Root = temp;
        }
        return pipeline;
    }
}
