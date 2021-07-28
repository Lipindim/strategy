using UnityEngine;

public class ProduceUnitCommand : IProduceUnitCommand
{
	public GameObject UnitPrefab => _unitPrefab;
	[InjectAsset("ChomperPrefab")] private GameObject _unitPrefab;

	public ProduceUnitCommand()
	{
	}
}
