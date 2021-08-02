using UnityEngine;


public class UnitPatrol : CommandExecutorBase<IPatrolCommand>

{
	public override void ExecuteSpecificCommand(IPatrolCommand command)
	{
		Debug.Log($"{name} is patrol from {command.Initial} to {command.Target}!");
	}
}

