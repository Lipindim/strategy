using UnityEngine;

public class ProduceUnitCommand : IProduceUnitCommand
{
	public GameObject UnitPrefab => _unitPrefab;
	protected GameObject _unitPrefab;

	public ProduceUnitCommand(GameObject unitPrefab)
	{
		_unitPrefab = unitPrefab;
	}
}
