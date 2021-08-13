using UnityEngine;

public class MainBuilding :MonoBehaviour, ISelectable, IAttackable
{
	public float Health => _health;
	public float MaxHealth => _maxHealth;
	public Sprite Icon => _icon;
	public string Name => gameObject.name;
	public Transform PivotPoint => transform;

    [SerializeField] private Transform _unitsParent;

	[SerializeField] private float _maxHealth = 1000;
	[SerializeField] private Sprite _icon;

	private float _health = 1000;

	//public override void ExecuteSpecificCommand(IProduceUnitCommand command)
	//{
	//	Instantiate(command.UnitPrefab, new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)), Quaternion.identity, _unitsParent);
	//}
}
