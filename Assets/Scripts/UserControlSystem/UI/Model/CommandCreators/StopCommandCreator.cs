using System;
using Zenject;

public class StopCommandCreator : CommandCreatorBase<IStopCommand>
{
	protected override void StartCommand(Action<IStopCommand> creationCallback)
	{
		creationCallback?.Invoke(new StopCommand());
	}
}