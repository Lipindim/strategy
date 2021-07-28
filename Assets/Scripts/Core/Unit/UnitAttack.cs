using UnityEngine;


public class UnitAttack : CommandExecutorBase<IAttackCommand>

{
	public override void ExecuteSpecificCommand(IAttackCommand command)
	{
		Debug.Log("Атакую!");
	}
}

