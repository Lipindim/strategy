using System;
using System.Threading;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public partial class UnitAttack : CommandExecutorBase<IAttackCommand>
{
    [SerializeField] private Animator _animator;
    [SerializeField] private UnitStop _stopCommandExecutor;
    [SerializeField] private ChoperUnit _unit;
    [SerializeField] private float _attackingDistance;
    [SerializeField] private int _attackingPeriod;

    public float AttackingDistance => _attackingDistance;
    public int AttackingPeriod => _attackingPeriod;
    public Vector3 OurPosition => _ourPosition;
    public Vector3 TargetPosition => _targetPosition;
    public Quaternion OurRotation => _ourRotation;
    private Vector3 _ourPosition;
    private Vector3 _targetPosition;
    private Quaternion _ourRotation;

    public Subject<Vector3> TargetPositions { get; } = new Subject<Vector3>();
    public Subject<Quaternion> TargetRotations { get; } = new Subject<Quaternion>();
    public Subject<IAttackable> AttackTargets { get; } = new Subject<IAttackable>();

    private Transform _targetTransform;
    private AttackOperation _currentAttackOp;

    public float Health => _unit.Health;

    private void Start()
    {
        TargetPositions
            .Select(value => new Vector3((float)Math.Round(value.x, 2), (float)Math.Round(value.y, 2), (float)Math.Round(value.z, 2)))
            .Distinct()
            .ObserveOnMainThread()
            .Subscribe(startMovingToPosition);

        AttackTargets
            .ObserveOnMainThread()
            .Subscribe(startAttackingTargets);

        TargetRotations
            .ObserveOnMainThread()
            .Subscribe(setAttackRoation);
    }

    private void setAttackRoation(Quaternion targetRotation)
    {
        transform.rotation = targetRotation;
    }

    private void startAttackingTargets(IAttackable target)
    {
        GetComponent<NavMeshAgent>().isStopped = true;
        GetComponent<NavMeshAgent>().ResetPath();
        _animator.SetTrigger("Attack");
        target.ReceiveDamage(GetComponent<IDamageDealer>().Damage);
    }

    private void startMovingToPosition(Vector3 position)
    {
        GetComponent<NavMeshAgent>().destination = position;
        _animator.SetTrigger(Animator.StringToHash("Walk"));
    }

    public override async Task ExecuteSpecificCommand(IAttackCommand command)
    {
        _targetTransform = (command.Target as Component).transform;
        _currentAttackOp = new AttackOperation(this, command.Target);
        Update();
        _stopCommandExecutor.CancellationTokenSource = new CancellationTokenSource();
        try
        {
            await _currentAttackOp.WithCancellation(_stopCommandExecutor.CancellationTokenSource.Token);
        }
        catch
        {
            _currentAttackOp.Cancel();
        }
        _animator.SetTrigger("Idle");
        _currentAttackOp = null;
        _targetTransform = null;
        _stopCommandExecutor.CancellationTokenSource = null;
    }

    private void Update()
    {
        if (_currentAttackOp == null)
        {
            return;
        }

        lock (this)
        {
            _ourPosition = transform.position;
            _ourRotation = transform.rotation;
            if (_targetTransform != null)
            {
                _targetPosition = _targetTransform.position;
            }
        }
    }
}