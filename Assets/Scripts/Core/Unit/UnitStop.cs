using UnityEngine;


public class UnitStop : CommandExecutorBase<IStopCommand>

{
	public override void ExecuteSpecificCommand(IStopCommand command)
	{
		Debug.Log("Стою!");
	}
}

