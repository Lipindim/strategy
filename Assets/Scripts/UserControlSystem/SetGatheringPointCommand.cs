using UnityEngine;


public class SetGatheringPointCommand : ISetGatheringPointCommand
{
    public Vector3 GatheringPoint { get; }

    public SetGatheringPointCommand(Vector3 gatheringPoint)
    {
        GatheringPoint = gatheringPoint;
    }
}

