using System.Collections;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.AI;


public class UnitMove : CommandExecutorBase<IMoveCommand>
{
    public bool IsMoving => _isMoving;


    [SerializeField] private UnitMovementStop _stop;
    [SerializeField] private Animator _animator;
    [SerializeField] private UnitStop _stopCommandExecutor;

    private bool _isMoving = false;

    public async override Task ExecuteSpecificCommand(IMoveCommand command)
    {
        _isMoving = true;
        GetComponent<NavMeshAgent>().destination = command.Target;
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
        }
        _stopCommandExecutor.CancellationTokenSource = null;
        _animator.SetTrigger("Idle");
        _isMoving = false;
    }

}