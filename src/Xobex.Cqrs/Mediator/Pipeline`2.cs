// <copyright file="Pipeline`2.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

namespace Xobex.Mediator;

public class Pipeline<TRequest, TResult>
    where TRequest : IRequest<TResult>
{
    private readonly PipelineEntry _root;
    
    internal Pipeline(PipelineEntry pipelineEntry)
    {
        _root = pipelineEntry;
    }

    public Task<TResult> RunAsync(IRequest<TResult> request, CancellationToken cancellationToken)
    {
        PipelineEntry? temp = _root;
        while(temp != null)
        {
            temp.Request = request;
            temp.Token = cancellationToken;
            temp = temp.Next;
        }
        return _root.Step();
    }
    
    internal class PipelineEntry
    {
        public PipelineEntry(Func<IRequest<TResult>, CancellationToken, Task<TResult>> func)
        {
            Step = () =>
            {
                return func(Request!, (CancellationToken)Token!);
            };
        }

        public PipelineEntry(Func<IRequest<TResult>, Func<Task<TResult>>?, CancellationToken, Task<TResult?>> func)
        {
            Step = () =>
            {
                return func(Request!, Next?.Step, (CancellationToken)Token!)!;
            };
        }

        public IRequest<TResult>? Request { get; set; }
        public CancellationToken? Token { get; set; }
        public PipelineEntry? Next { get; set; }
        public Func<Task<TResult>> Step { get; }
    }
}
