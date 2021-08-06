using System;


public abstract class CommandCreatorBase<T> where T : ICommand
{
    protected bool _pending = false;

    public ICommandExecutor ProcessCommandExecutor(ICommandExecutor commandExecutor, Action<T> callback)
    {
        var classSpecificExecutor = commandExecutor as CommandExecutorBase<T>;
        if (classSpecificExecutor != null)
        {
            StartCommand(callback);
        }
        return commandExecutor;
    }

    protected virtual void StartCommand(Action<T> creationCallback)
    {
        _pending = true;
    }

    public virtual void ProcessCancel()
    {
        _pending = false;
    }
}