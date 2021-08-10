using System;


public class AttackCommandCreator : CancellableCommandCreatorBase<IAttackCommand, IAttackable>
{

	private Action<IAttackCommand> _creationCallback;

    private void OnTargetSelected(IAttackable target)
    {
        if (_pending)
        {
			_creationCallback?.Invoke(new AttackCommand(target));
			_creationCallback = null;
		}
    }


	public override void ProcessCancel()
	{
		base.ProcessCancel();

		_creationCallback = null;
	}

    protected override IAttackCommand CreateCommand(IAttackable argument)
    {
		return new AttackCommand(argument);
    }
}