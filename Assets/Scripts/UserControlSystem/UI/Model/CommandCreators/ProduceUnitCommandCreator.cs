using System;
using Zenject;

public class ProduceUnitCommandCreator : CommandCreatorBase<IProduceUnitCommand>
{
	//[Inject] private AssetsContext _context;
	[Inject] private DiContainer _diContainer;

	protected override void StartCommand(Action<IProduceUnitCommand> creationCallback)
	{
		var produceUnitCommand = new ProduceUnitCommand();
		_diContainer.Inject(produceUnitCommand);
		creationCallback?.Invoke(produceUnitCommand);
	}
}