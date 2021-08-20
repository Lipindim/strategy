using UniRx;
using UnityEngine;
using Zenject;


public class ChomperCommandsQueue : MonoBehaviour, ICommandsQueue
{
	[SerializeField] CommandExecutorBase<IMoveCommand> _moveCommandExecutor;
	[SerializeField] CommandExecutorBase<IPatrolCommand> _patrolCommandExecutor;
	[SerializeField] CommandExecutorBase<IAttackCommand> _attackCommandExecutor;
	[SerializeField] CommandExecutorBase<IStopCommand> _stopCommandExecutor;

	private ReactiveCollection<ICommand> _innerCollection = new ReactiveCollection<ICommand>();

	public ICommand CurrentCommand => _innerCollection.Count > 0 ? _innerCollection[0] : default;

	private void Start()
	{
		_innerCollection
			.ObserveAdd().Subscribe(onNewCommand).AddTo(this);
	}

	private void onNewCommand(ICommand command, int index)
	{
		if (index == 0)
		{
			executeCommand(command);
		}
	}

	private async void executeCommand(ICommand command)
	{
		await _moveCommandExecutor.TryExecuteCommand(command);
		await _patrolCommandExecutor.TryExecuteCommand(command);
		await _attackCommandExecutor.TryExecuteCommand(command);
		await _stopCommandExecutor.TryExecuteCommand(command);
		if (_innerCollection.Count > 0)
		{
			_innerCollection.RemoveAt(0);
		}
		checkTheQueue();
	}

	private void checkTheQueue()
	{
		if (_innerCollection.Count > 0)
		{
			executeCommand(_innerCollection[0]);
		}
	}

	public void EnqueueCommand(object wrappedCommand)
	{
		var command = wrappedCommand as ICommand;
		_innerCollection.Add(command);
	}

	public void Clear()
	{
		_innerCollection.Clear();
		_stopCommandExecutor.ExecuteSpecificCommand(new StopCommand());
	}
}