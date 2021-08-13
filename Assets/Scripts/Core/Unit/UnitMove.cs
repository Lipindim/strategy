using System.Collections;
using System.Linq;
using System.Threading;
using UniRx;
using UnityEngine;
using UnityEngine.AI;


public class UnitMove : CommandExecutorBase<IMoveCommand>
{
    public bool IsMoving => _isMoving;

    private ReactiveCollection<IMoveCommand> _queue = new ReactiveCollection<IMoveCommand>();

    [SerializeField] private UnitMovementStop _stop;
    [SerializeField] private Animator _animator;
    [SerializeField] private UnitStop _stopCommandExecutor;

    private bool _isMoving;

    public override void ExecuteSpecificCommand(IMoveCommand command)
    {
        if (command.Defer)
        {
            _queue.Add(command);
        }
        else
        {
            if (_isMoving)
                _stopCommandExecutor.CancellationTokenSource.Cancel();
            ExecuteMoveCommand(command);
        }
    }

    private void Update()
    {
        if (_queue.Count == 0)
            return;

        if (_isMoving)
            return;

        StartCoroutine(StartCommand());
    }

    private IEnumerator StartCommand()
    {
        var executeComand = _queue.First();
        _queue.Remove(executeComand);
        ExecuteMoveCommand(executeComand);
        yield return null;
    }

    private async void ExecuteMoveCommand(IMoveCommand command)
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
            _queue.Clear();
        }
        _stopCommandExecutor.CancellationTokenSource = null;
        _animator.SetTrigger("Idle");
        _isMoving = false;
    }

}