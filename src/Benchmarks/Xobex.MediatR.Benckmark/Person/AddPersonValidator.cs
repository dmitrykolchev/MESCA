// <copyright file="AddPersonValidator.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

using MediatR.Pipeline;

namespace Xobex.MediatR.Benchmark.Person;
public class AddPersonValidator : IRequestPreProcessor<AddPersonCommand>
{
    public Task Process(AddPersonCommand request, CancellationToken cancellationToken)
    {
        return string.IsNullOrEmpty(request.FirstName) || string.IsNullOrEmpty(request.LastName)
            ? throw new ArgumentException("FirstName and LastName must be set")
            : Task.CompletedTask;
    }
}
