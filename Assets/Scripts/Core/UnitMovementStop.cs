using System;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

public class UnitMovementStop : MonoBehaviour, IAwaitable<AsyncExtensions.Void>
{
    public class StopAwaiter : AwaiterBase<AsyncExtensions.Void>
    {
        private readonly UnitMovementStop _unitMovementStop;

        public StopAwaiter(UnitMovementStop unitMovementStop)
        {
            _unitMovementStop = unitMovementStop;
            _unitMovementStop.Stoped += onStop;
        }

        private void onStop()
        {
            _unitMovementStop.Stoped -= onStop;
            onWaitFinish(new AsyncExtensions.Void());
        }
    }

    public event Action Stoped;

    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private UnitStopDetector _unitStopDetector;
    [SerializeField] private int _throttleFrames = 60;
    [SerializeField] private int _continuityThreshold = 10;


    private void Awake()
    {
        _unitStopDetector.StopDetected += OnStopDetected;
    }

    private void OnStopDetected()
    {
        Stoped?.Invoke();
    }

    private void Update()
    {
        if (!_agent.pathPending)
        {
            if (_agent.remainingDistance <= _agent.stoppingDistance)
            {
                if (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f)
                {
                    Stoped?.Invoke();
                }
            }
        }
    }

    public IAwaiter<AsyncExtensions.Void> GetAwaiter() => new StopAwaiter(this);
}