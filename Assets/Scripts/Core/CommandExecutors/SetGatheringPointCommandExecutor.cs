﻿using UnityEngine;


public class SetGatheringPointCommandExecutor : CommandExecutorBase<ISetGatheringPointCommand>
{
    public Vector3 GatheringPoint => _gatheringPoint.position;

    [SerializeField] private Transform _gatheringPoint;

    public override void ExecuteSpecificCommand(ISetGatheringPointCommand command)
    {
        _gatheringPoint.position = command.GatheringPoint;
    }
}

