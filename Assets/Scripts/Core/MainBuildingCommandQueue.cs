using UnityEngine;
using Zenject;

public class MainBuildingCommandQueue : MonoBehaviour, ICommandsQueue
{
	[SerializeField] CommandExecutorBase<IProduceUnitCommand> _produceUnitCommandExecutor;
	[SerializeField] CommandExecutorBase<ISetGatheringPointCommand> _setGatheringPointCommand;

	public void Clear() { }

	public async void EnqueueCommand(object command)
	{
		await _produceUnitCommandExecutor.TryExecuteCommand(command);
		await _setGatheringPointCommand.TryExecuteCommand(command);
	}
}