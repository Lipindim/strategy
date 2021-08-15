using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class UnitPatrol : CommandExecutorBase<IPatrolCommand>
{
    [SerializeField] private UnitMovementStop _stop;
    [SerializeField] private Animator _animator;
    [SerializeField] private UnitStop _stopCommandExecutor;

    private Vector3 _destinationPath;
    
    public async override Task ExecuteSpecificCommand(IPatrolCommand command)
    {
        _destinationPath = command.Target;
        bool stopped = false;
        while (!stopped)
        {
            GetComponent<NavMeshAgent>().destination = _destinationPath;
            _animator.SetTrigger("Walk");
            _stopCommandExecutor.CancellationTokenSource = new CancellationTokenSource();
            try
            {
                await _stop
                .WithCancellation
                    (
                    _stopCommandExecutor
                        .CancellationTokenSource
                        .Token
                    );
            }
            catch
            {
                GetComponent<NavMeshAgent>().isStopped = true;
                GetComponent<NavMeshAgent>().ResetPath();
                _animator.SetTrigger("Idle");
                stopped = true;
            }
            _destinationPath = _destinationPath == command.Target ? command.Initial : command.Target;
            _stopCommandExecutor.CancellationTokenSource = null;
        }
    }
}

