using UnityEngine.AI;


public class UnitStop : CommandExecutorBase<IStopCommand>

{
	public override void ExecuteSpecificCommand(IStopCommand command)
	{
		GetComponent<NavMeshAgent>().ResetPath();
	}
}

