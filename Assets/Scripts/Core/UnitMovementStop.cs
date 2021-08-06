using System;
using UnityEngine;
using UnityEngine.AI;

public class UnitMovementStop : MonoBehaviour, IAwaitable<AsyncExtensions.Void>
{
    public class StopAwaiter : IAwaiter<AsyncExtensions.Void>
    {
        private readonly UnitMovementStop _unitMovementStop;
        private Action _continuation;
        private bool _isCompleted;

        public StopAwaiter(UnitMovementStop unitMovementStop)
        {
            _unitMovementStop = unitMovementStop;
            _unitMovementStop.Stoped += OnStop;
        }

        private void OnStop()
        {
            _unitMovementStop.Stoped -= OnStop;
            _isCompleted = true;
            _continuation?.Invoke();
        }

        public void OnCompleted(Action continuation)
        {
            if (_isCompleted)
            {
                continuation?.Invoke();
            }
            else
            {
                _continuation = continuation;
            }
        }
        public bool IsCompleted => _isCompleted;
        public AsyncExtensions.Void GetResult() => new AsyncExtensions.Void();
    }

    public event Action Stoped;

    [SerializeField] private NavMeshAgent _agent;

    void Update()
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