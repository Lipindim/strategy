using UnityEngine;


public interface IPatrolCommand : ICommand
{
    public Vector3 Initial { get; }
    public Vector3 Target { get; }
}

