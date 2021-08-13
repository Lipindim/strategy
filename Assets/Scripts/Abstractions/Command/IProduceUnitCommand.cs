using UnityEngine;

public interface IProduceUnitCommand : ICommand, IIcon
{
	float ProductionTime { get; }
	GameObject UnitPrefab { get; }
	string UnitName { get; }
}
