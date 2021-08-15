using UnityEngine;
using Zenject;

public class ProduceUnitCommand : IProduceUnitCommand
{
	[Inject(Id = ObjectIdentifiers.Chomper)] public string UnitName { get; }
	[Inject(Id = ObjectIdentifiers.Chomper)] public Sprite Icon { get; }
	[Inject(Id = ObjectIdentifiers.Chomper)] public float ProductionTime { get; }
	[Inject(Id = ObjectIdentifiers.Chomper)] public GameObject UnitPrefab { get; }

	//public GameObject UnitPrefab => _unitPrefab;
	//[InjectAsset(ObjectIdentifiers.Chomper)] private GameObject _unitPrefab;
}
