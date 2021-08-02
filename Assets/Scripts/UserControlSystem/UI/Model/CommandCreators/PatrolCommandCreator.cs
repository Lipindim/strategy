using System;
using UnityEngine;
using Zenject;

public class PatrolCommandCreator : CommandCreatorBase<IPatrolCommand>
{
	private Action<IPatrolCommand> _creationCallback;
	private Vector3Value _initialPosition;


	[Inject]
	private void Init([Inject(Id = ObjectIdentifiers.GroundClickPoint)] Vector3Value groundClickPoint,
		[Inject(Id = ObjectIdentifiers.InitialPoint)] Vector3Value initialPosition)
	{
		groundClickPoint.ValueChanged += ExecuteCommand;
		_initialPosition = initialPosition;
	}

	private void ExecuteCommand(Vector3 groundClickPoint)
	{
		if (_pending)
		{
			_creationCallback?.Invoke(new PatrolCommand(_initialPosition.CurrentValue, groundClickPoint));
			_creationCallback = null;
		}
	}

	protected override void CreateCommand(Action<IPatrolCommand> creationCallback)
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