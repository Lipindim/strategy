using System;
using UnityEngine;


[Serializable]
public class ProduceUnitCommand : IProduceUnitCommand
{
	[SerializeField] private string _unitName;
	[SerializeField] private Sprite _icon;
	[SerializeField] private float _productionTime;
	[SerializeField] private GameObject _unitPrefab;

	public string UnitName => _unitName;
	public Sprite Icon => _icon;
	public float ProductionTime => _productionTime;
	public GameObject UnitPrefab => _unitPrefab;
}
