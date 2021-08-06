using System;
using UnityEngine;
using Zenject;

public class ProduceUnitCommandCreator : CommandCreatorBase<IProduceUnitCommand>
{
    private readonly GameObject _unitPrefab;

	[Inject]
    public ProduceUnitCommandCreator([Inject(Id = ObjectIdentifiers.ChomperPrefab)] GameObject unitPrefab)
    {
		_unitPrefab = unitPrefab;
    }

	protected override void StartCommand(Action<IProduceUnitCommand> creationCallback)
	{
		creationCallback?.Invoke(new ProduceUnitCommandHeir(_unitPrefab));
	}
}