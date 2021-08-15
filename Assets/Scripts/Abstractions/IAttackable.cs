using System;


public interface IAttackable : IHealth
{
    event Action Dead;
    void ReceiveDamage(int amount);
}

