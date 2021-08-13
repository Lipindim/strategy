using System;
using UnityEngine;
using Zenject;

public class PatrolCommandCreator : CancellableCommandCreatorBase<IPatrolCommand, Vector3>
{
	[Inject] private IObservable<ISelectable> _selectable;

	protected override IPatrolCommand CreateCommand(Vector3 argument) => new PatrolCommand( new Vector3(), argument);
}