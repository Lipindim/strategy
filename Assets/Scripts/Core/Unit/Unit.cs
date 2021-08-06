using UnityEngine;

public class Unit : MonoBehaviour, ISelectable, IAttackable
{
	public float Health => _health;
	public float MaxHealth => _maxHealth;
	public Sprite Icon => _icon;
	public string Name => gameObject.name;
	public Transform PivotPoint => transform;

    [SerializeField] private float _maxHealth = 100;
	[SerializeField] private Sprite _icon;

	private float _health = 100;

}
