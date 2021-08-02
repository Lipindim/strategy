using System;
using Zenject;


public class AttackCommandCreator : CommandCreatorBase<IAttackCommand>
{

	private Action<IAttackCommand> _creationCallback;

	[Inject]
	private void Init([Inject(Id = ObjectIdentifiers.SelectedTarget)] SelectableValue selectedTarget)
	{
        selectedTarget.ValueChanged += OnTargetSelected;
	}

    private void OnTargetSelected(ISelectable target)
    {
        if (_pending)
        {
			_creationCallback?.Invoke(new AttackCommand(target));
			_creationCallback = null;
		}
    }

	protected override void CreateCommand(Action<IAttackCommand> creationCallback)
	{
		base.CreateCommand(creationCallback);

		_creationCallback = creationCallback;
	}

	public override void ProcessCancel()
	{
		base.ProcessCancel();

		_creationCallback = null;
	}
}