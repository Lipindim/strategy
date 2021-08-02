using System;
using Zenject;

public class StopCommandCreator : CommandCreatorBase<IStopCommand>
{
	protected override void CreateCommand(Action<IStopCommand> creationCallback)
	{
		creationCallback?.Invoke(new StopCommand());
	}
}