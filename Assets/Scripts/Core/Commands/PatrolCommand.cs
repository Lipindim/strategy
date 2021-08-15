using UnityEngine;

public class PatrolCommand : IPatrolCommand
{
	public Vector3 Initial { get; }
	public Vector3 Target { get; }

	public PatrolCommand(Vector3 initial, Vector3 target)
	{
		Initial = initial;
		Target = target;
	}
}