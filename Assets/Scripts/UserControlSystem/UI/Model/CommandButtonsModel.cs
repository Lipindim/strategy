using System;
using UniRx;
using Zenject;

public class CommandButtonsModel
{

	public IObservable<ICommandExecutor> CommandAccepted => _commandAccepted;
	private Subject<ICommandExecutor> _commandAccepted = new Subject<ICommandExecutor>();

	public IObservable<Unit> CommandSent => _commandSent;
	private ReactiveCommand _commandSent = new ReactiveCommand();

	public IObservable<Unit> CommandCancel => _commandCancel;
	private ReactiveCommand _commandCancel = new ReactiveCommand();

	[Inject] private CommandCreatorBase<IProduceUnitCommand> _unitProducer;
	[Inject] private CommandCreatorBase<IAttackCommand> _attacker;
	[Inject] private CommandCreatorBase<IStopCommand> _stopper;
	[Inject] private CommandCreatorBase<IMoveCommand> _mover;
	[Inject] private CommandCreatorBase<IPatrolCommand> _patroller;

	private bool _commandIsPending;

	public void OnCommandButtonClicked(ICommandExecutor commandExecutor)
	{
		if (_commandIsPending)
		{
			CancelProcess();
		}
		_commandIsPending = true;
		_commandAccepted.OnNext(commandExecutor);

		_unitProducer.ProcessCommandExecutor(commandExecutor, command => ExecuteCommandWrapper(commandExecutor, command));
		_attacker.ProcessCommandExecutor(commandExecutor, command => ExecuteCommandWrapper(commandExecutor, command));
		_stopper.ProcessCommandExecutor(commandExecutor, command => ExecuteCommandWrapper(commandExecutor, command));
		_mover.ProcessCommandExecutor(commandExecutor, command => ExecuteCommandWrapper(commandExecutor, command));
		_patroller.ProcessCommandExecutor(commandExecutor, command => ExecuteCommandWrapper(commandExecutor, command));
	}

	public void ExecuteCommandWrapper(ICommandExecutor commandExecutor, object command)
	{
		commandExecutor.ExecuteCommand(command);
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