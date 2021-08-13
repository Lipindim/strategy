using UnityEngine;


public interface ISetGatheringPointCommand : ICommand
{
    Vector3 GatheringPoint { get; }
}

