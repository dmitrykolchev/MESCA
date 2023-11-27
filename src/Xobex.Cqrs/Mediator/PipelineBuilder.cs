// <copyright file="PipelineBuilder.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

namespace Xobex.Mediator;

public class PipelineBuilder<TRequest, TResult>
    where TRequest : IRequest<TResult>
{
    private readonly Queue<IBehavior<TRequest, TResult>> _behaviors = new();

    public PipelineBuilder(IMediatorService mediatorService)
    {
        MediatorService = mediatorService ?? throw new ArgumentNullException(nameof(mediatorService));
    }

    public IMediatorService MediatorService { get; }

    public PipelineBuilder<TRequest, TResult> Use(IBehavior<TRequest, TResult> behavior)
    {
        ArgumentNullException.ThrowIfNull(behavior);
        _behaviors.Enqueue(behavior);
        return this;
    }

    public Pipeline<TRequest, TResult> Build()
    {
        Pipeline<TRequest, TResult>.PipelineEntry pipelineEntry = new (MediatorService.QueryAsync);
        while (_behaviors.Count > 0)
        {
            IBehavior<TRequest, TResult> behavior = _behaviors.Dequeue();
            Pipeline<TRequest, TResult>.PipelineEntry temp = new (behavior.ProcessAsync);
            temp.Next = pipelineEntry;
            pipelineEntry = temp;
        }
        return new Pipeline<TRequest, TResult>(pipelineEntry);
    }
}
