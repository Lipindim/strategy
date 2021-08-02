using UnityEngine;


public class UnitAttack : CommandExecutorBase<IAttackCommand>
{
	public override void ExecuteSpecificCommand(IAttackCommand command)
	{
		var targetObject = (MonoBehaviour)command.Target;
		Debug.Log($"Атакую {targetObject.name}!");
	}
}

