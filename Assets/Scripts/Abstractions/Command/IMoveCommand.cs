using UnityEngine;

public interface IMoveCommand : IDeferredCommand
{
	public Vector3 Target { get; }
}