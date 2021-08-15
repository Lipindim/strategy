using System;
using UnityEngine;

public class ChoperUnit : MonoBehaviour, ISelectable, IAttackable, IUnit, IDamageDealer
{
    public event Action Dead;
    public float Health => _health;
	public float MaxHealth => _maxHealth;
	public Sprite Icon => _icon;
	public string Name => gameObject.name;
	public Transform PivotPoint => transform;

    public int Damage => _damage;

    [SerializeField] private Animator _animator;
	[SerializeField] private UnitStop _stopCommand;
	[SerializeField] private Sprite _icon;
	[SerializeField] private float _maxHealth = 100;
    [SerializeField] private int _damage = 25;
	

	private float _health = 100;

    public void ReceiveDamage(int amount)
    {
        if (_health <= 0)
            return;
        _health -= amount;
        if (_health <= 0)
        {
            _animator.SetTrigger("Dead");
            Invoke(nameof(DestroyUnit), 1f);
        }
    }

    private async void DestroyUnit()
    {
        await _stopCommand.ExecuteSpecificCommand(new StopCommand());
        Dead?.Invoke();
        Destroy(gameObject);
    }
}
