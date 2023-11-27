// <copyright file="Pipeline`2.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

namespace Xobex.Mediator;

public class Pipeline<TResult>
{
    private PipelineEntry? _root;
    private IRequest<TResult>? _request;
    private CancellationToken? _token;

    internal Pipeline()
    {
    }

    internal PipelineEntry? Root
    {
        get => _root;
        set => _root = value;
    }

    public Task<TResult> RunAsync(IRequest<TResult> request, CancellationToken cancellationToken)
    {
        _request = request;
        _token = cancellationToken;
        return _root!.Step();
    }

    internal class PipelineEntry
    {
        private readonly Pipeline<TResult> _parent;
        private readonly IBehavior<TResult>? _behavior;

        public PipelineEntry(Pipeline<TResult> parent, Func<IRequest<TResult>, CancellationToken, Task<TResult>> func)
        {
            _parent = parent;
            Step = () =>
            {
                return func(Request!, (CancellationToken)Token!);
            };
        }

        public PipelineEntry(Pipeline<TResult> parent, IBehavior<TResult> behavior)
        {
            _parent = parent;
            _behavior = behavior;
            Step = () =>
            {
                return _behavior.ProcessAsync(Request!, Next?.Step, (CancellationToken)Token!)!;
            };
        }

        public IRequest<TResult>? Request => _parent._request;
        public CancellationToken? Token => _parent._token;
        public Func<Task<TResult>> Step { get; }
        public PipelineEntry? Next { get; set; }
    }
}
