using System.Runtime.CompilerServices;


public interface IAwaiter<out T> : INotifyCompletion
{
    bool IsCompleted { get; }

    T GetResult();
}
