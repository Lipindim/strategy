using System;
using UniRx;
using UnityEngine;
using Zenject;


public class CommandButtonsModel
{
	public IObservable<ICommandExecutor> CommandAccepted => _commandAccepted;
	private Subject<ICommandExecutor> _commandAccepted = new Subject<ICommandExecutor>();

	public IObservable<Unit> CommandSent => _commandSent;
	private ReactiveCommand _commandSent = new ReactiveCommand();

	public IObservable<Unit> CommandCancel => _commandCancel;
	private ReactiveCommand _commandCancel = new ReactiveCommand();

	public bool CommandIsPending => _commandIsPending;

	[Inject] private CommandCreatorBase<IProduceUnitCommand> _unitProducer;
	[Inject] private CommandCreatorBase<IAttackCommand> _attacker;
	[Inject] private CommandCreatorBase<IStopCommand> _stopper;
	[Inject] private CommandCreatorBase<IMoveCommand> _mover;
	[Inject] private CommandCreatorBase<IPatrolCommand> _patroller;
	[Inject] private CommandCreatorBase<ISetGatheringPointCommand> _gatheringPointSetter;

	private bool _commandIsPending;

	public void OnCommandButtonClicked(ICommandExecutor commandExecutor, ICommandsQueue commandsQueue)
	{
		if (_commandIsPending)
			CancelProcess();

		_commandIsPending = true;
		_commandAccepted.OnNext(commandExecutor);

		_unitProducer.ProcessCommandExecutor(commandExecutor, command => ExecuteCommandWrapper(command, commandsQueue));
		_attacker.ProcessCommandExecutor(commandExecutor, command => ExecuteCommandWrapper(command, commandsQueue));
		_stopper.ProcessCommandExecutor(commandExecutor, command => ExecuteCommandWrapper(command, commandsQueue));
		_mover.ProcessCommandExecutor(commandExecutor, command => ExecuteCommandWrapper(command, commandsQueue));
		_patroller.ProcessCommandExecutor(commandExecutor, command => ExecuteCommandWrapper(command, commandsQueue));
		_gatheringPointSetter.ProcessCommandExecutor(commandExecutor, command => ExecuteCommandWrapper(command, commandsQueue));
	}

	public void ExecuteCommandWrapper(object command, ICommandsQueue commandsQueue)
	{
		if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
			commandsQueue.Clear();
		
		commandsQueue.EnqueueCommand(command);
		_commandIsPending = false;
		_commandSent.Execute();
	}

	public void OnSelectionChanged()
	{
		_commandIsPending = false;
		CancelProcess();
	}

	private void CancelProcess()
	{
		_unitProducer.ProcessCancel();
		_attacker.ProcessCancel();
		_stopper.ProcessCancel();
		_mover.ProcessCancel();
		_patroller.ProcessCancel();

		_commandCancel.Execute();
	}
}