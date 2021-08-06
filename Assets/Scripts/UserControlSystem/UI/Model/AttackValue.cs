using UnityEngine;


[CreateAssetMenu(fileName = nameof(AttackValue), menuName = "Strategy Game/" + nameof(AttackValue), order = 1)]
public class AttackValue : ScriptableObjectValueBase<IAttackable>
{
}

