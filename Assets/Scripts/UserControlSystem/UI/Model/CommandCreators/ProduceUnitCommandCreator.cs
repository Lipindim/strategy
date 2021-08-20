using System;
using Zenject;

public class ProduceUnitCommandCreator : CommandCreatorBase<IProduceUnitCommand>
{
	[Inject] private ProduceUnitCommandValue _produceUnitCommandValue;

	protected override void StartCommand(Action<IProduceUnitCommand> creationCallback)
	{
		creationCallback?.Invoke(_produceUnitCommandValue.CurrentValue);
	}
}