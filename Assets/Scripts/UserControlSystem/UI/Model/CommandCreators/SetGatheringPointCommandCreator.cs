using UnityEngine;


public class SetGatheringPointCommandCreator : CancellableCommandCreatorBase<ISetGatheringPointCommand, Vector3>
{
    public override ISetGatheringPointCommand CreateCommand(Vector3 argument) => new SetGatheringPointCommand(argument);
}

