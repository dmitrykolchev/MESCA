// <copyright file="PipelineBuilder.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using Microsoft.Extensions.DependencyInjection;

namespace Xobex.Mediator;

public class PipelineBuilder
{
    private readonly Stack<IBehavior> _behaviors = new();

    internal PipelineBuilder(IMediatorService mediatorService, IServiceProvider serviceProvider)
    {
        MediatorService = mediatorService ?? throw new ArgumentNullException(nameof(mediatorService));
        ServiceProvider = serviceProvider;
    }

    private IMediatorService MediatorService { get; }
    private IServiceProvider ServiceProvider { get; }

    /// <summary>
    /// Creates instance of TBehavior and adds to the pipeline
    /// </summary>
    /// <typeparam name="TBehavior"></typeparam>
    /// <returns></returns>
    public PipelineBuilder Use<TBehavior>()
        where TBehavior : IBehavior
    {
        return Use(ActivatorUtilities.CreateInstance<TBehavior>(ServiceProvider));
    }
    /// <summary>
    /// Adds behavior instance to the pipeline
    /// </summary>
    /// <param name="behavior"></param>
    /// <returns></returns>
    public PipelineBuilder Use(IBehavior behavior)
    {
        ArgumentNullException.ThrowIfNull(behavior);
        _behaviors.Push(behavior);
        return this;
    }

    public PipelineBuilder Use(Func<IRequest, Func<Task<object>>?, CancellationToken, Task<object?>> func)
    {
        ArgumentNullException.ThrowIfNull(func);

        _behaviors.Push(new InternalBehavior(func));
        return this;
    }
    /// <summary>
    /// Builds pipeline
    /// </summary>
    /// <returns></returns>
    public Pipeline Build()
    {
        Pipeline pipeline = new();
        pipeline.Root = new(pipeline, MediatorService.QueryAsync);
        while (_behaviors.Count > 0)
        {
            IBehavior behavior = _behaviors.Pop();
            Pipeline.PipelineEntry temp = new(pipeline, behavior)
            {
                Next = pipeline.Root
            };
            pipeline.Root = temp;
        }
        return pipeline;
    }

    private class InternalBehavior : IBehavior
    {
        private readonly Func<IRequest, Func<Task<object>>?, CancellationToken, Task<object?>> _func;
        public InternalBehavior(Func<IRequest, Func<Task<object>>?, CancellationToken, Task<object?>> func)
        {
            _func = func;
        }
        public Task<object?> ProcessAsync(IRequest request, Func<Task<object>>? next, CancellationToken cancellationToken)
        {
            return _func(request, next, cancellationToken);
        }
    }
}
