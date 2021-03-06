using UnityEngine;
using Zenject;


public class MoveCommandCreator : CancellableCommandCreatorBase<IMoveCommand, Vector3>
{
	[Inject] private BoolValue _deferValue;
	public override IMoveCommand CreateCommand(Vector3 argument) => new MoveCommand(argument, _deferValue.CurrentValue);
}