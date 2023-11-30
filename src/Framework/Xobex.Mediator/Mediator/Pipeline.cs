// <copyright file="Pipeline.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

namespace Xobex.Mediator;

public class Pipeline
{
    private PipelineEntry? _root;
    private IRequest? _request;
    private CancellationToken? _token;

    internal Pipeline()
    {
    }

    internal PipelineEntry? Root
    {
        get => _root;
        set => _root = value;
    }

    public Task<TResult?> RunAsync<TResult>(IRequest<TResult> request)
    {
        return RunAsync(request, CancellationToken.None);
    }

    public async Task<TResult?> RunAsync<TResult>(IRequest<TResult> request, CancellationToken cancellationToken)
    {
        _request = request;
        _token = cancellationToken;
        return (TResult?)await _root!.Step();
    }

    internal class PipelineEntry
    {
        private readonly Pipeline _parent;
        private readonly IBehavior? _behavior;

        public PipelineEntry(Pipeline parent, Func<IRequest, CancellationToken, Task<object>> func)
        {
            _parent = parent;
            Step = async () =>
            {
                return (await func(Request!, (CancellationToken)Token!))!;
            };
        }

        public PipelineEntry(Pipeline parent, IBehavior behavior)
        {
            _parent = parent;
            _behavior = behavior;
            Step = () =>
            {
                return _behavior.ProcessAsync(Request!, Next?.Step, (CancellationToken)Token!)!;
            };
        }

        public IRequest? Request => _parent._request;
        public CancellationToken? Token => _parent._token;
        public Func<Task<object>> Step { get; }
        public PipelineEntry? Next { get; set; }
    }
}
