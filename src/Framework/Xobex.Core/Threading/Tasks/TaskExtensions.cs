// <copyright file="TaskExtensions.cs" company="DykBits">
// (c) 2022-23 Dmitry Kolchev. All rights reserved.
// See LICENSE in the project root for license information
// </copyright>

namespace Xobex.Data.Threading.Tasks;

public static class TaskExtensions
{
    public static void FireAndForget(
        this Task task,
        in bool continueOfCapturedContext = false,
        in Action? onSuccess = null,
        in Action<Exception>? onException = null)
    {
        _ = task ?? throw new ArgumentNullException(nameof(task));
        InvokeFireAndForget(task, continueOfCapturedContext, onSuccess, onException);
    }

    public static void FireAndForget<TException>(
        this Task task,
        in bool continueOfCapturedContext = false,
        in Action? onSuccess = null,
        in Action<TException>? onException = null)
        where TException : Exception
    {
        _ = task ?? throw new ArgumentNullException(nameof(task));
        InvokeFireAndForget(task, continueOfCapturedContext, onSuccess, onException);
    }

    public static void FireAndForget(
        this ValueTask task,
        in bool continueOfCapturedContext = false,
        in Action<Exception>? onException = null)
    {
        InvokeFireAndForget(task, continueOfCapturedContext, onException);
    }

    public static void FireAndForget<TException>(
        this ValueTask task,
        in bool continueOfCapturedContext = false,
        in Action<TException>? onException = null)
        where TException : Exception
    {
        InvokeFireAndForget(task, continueOfCapturedContext, onException);
    }


    private static async void InvokeFireAndForget<TException>(
        Task task,
        bool continueOnCapturedContext,
        Action? onSuccess,
        Action<TException>? onException)
        where TException : Exception
    {
        try
        {
            await task.ConfigureAwait(continueOnCapturedContext);
            onSuccess?.Invoke();
        }
        catch (TException ex) when (onException != null)
        {
            onException.Invoke(ex);
        }
    }

    private static async void InvokeFireAndForget<TException>(
        ValueTask task,
        bool continueOnCapturedContext,
        Action<TException>? onException)
        where TException : Exception
    {
        try
        {
            await task.ConfigureAwait(continueOnCapturedContext);
        }
        catch (TException ex) when (onException != null)
        {
            onException.Invoke(ex);
        }
    }
}
