using UnityEngine;


public class UnitMove : CommandExecutorBase<IMoveCommand>
{
	public override void ExecuteSpecificCommand(IMoveCommand command)
	{
		Debug.Log("Двигаюсь!");
	}
}

