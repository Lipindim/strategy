using UnityEngine;

public class MoveCommand : IMoveCommand
{
	public Vector3 Target { get; }

    public bool Defer { get; }

    public MoveCommand(Vector3 target, bool defer)
	{
		Target = target;
		Defer = defer;
	}
}