using System;


public abstract class CommandCreatorBase<T> where T : ICommand
{
    protected bool _pending = false;

    public ICommandExecutor ProcessCommandExecutor(ICommandExecutor commandExecutor, Action<T> callback)
    {
        var classSpecificExecutor = commandExecutor as CommandExecutorBase<T>;
        if (classSpecificExecutor != null)
        {
            CreateCommand(callback);
        }
        return commandExecutor;
    }

    protected virtual void CreateCommand(Action<T> creationCallback)
    {
        _pending = true;
    }

    public virtual void ProcessCancel()
    {
        _pending = false;
    }
}