using UnityEngine;
using Zenject;


public class MoveCommandCreator : CancellableCommandCreatorBase<IMoveCommand, Vector3>
{
	[Inject] private BoolValue _deferValue;
	protected override IMoveCommand CreateCommand(Vector3 argument) => new MoveCommand(argument, _deferValue.CurrentValue);
}