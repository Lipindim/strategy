using System;
using UnityEngine;
using Zenject;

public class MoveCommandCreator : CommandCreatorBase<IMoveCommand>
{
	private Action<IMoveCommand> _creationCallback;

	[Inject]
	private void Init([Inject(Id = ObjectIdentifiers.GroundClickPoint)] Vector3Value groundClick)
	{
		groundClick.ValueChanged += ExecuteCommand;
	}

	private void ExecuteCommand(Vector3 groundClick)
	{
		if (_pending)
		{
			_creationCallback?.Invoke(new MoveCommand(groundClick));
			_creationCallback = null;
		}
	}

	protected override void CreateCommand(Action<IMoveCommand> creationCallback)
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