using System;
using UnityEngine;

public class MainBuilding : MonoBehaviour, ISelectable, IAttackable, IUnit, IDamageDealer
{
	public event Action Dead;
	public float Health => _health;
	public float MaxHealth => _maxHealth;
	public Sprite Icon => _icon;
	public string Name => gameObject.name;
	public Transform PivotPoint => transform;

	public int Damage => _damage;

    [SerializeField] private Transform _unitsParent;
	[SerializeField] private Sprite _icon;

	[SerializeField] private int _damage;
	[SerializeField] private float _maxHealth = 1000;


	private float _health = 1000;


    public void ReceiveDamage(int amount)
	{
		if (_health <= 0)
			return;

		_health -= amount;
		if (_health <= 0)
		{
			Dead?.Invoke();
			Destroy(gameObject);
		}
	}
}
