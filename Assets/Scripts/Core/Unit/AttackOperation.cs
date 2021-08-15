using System;
using System.Threading;
using UnityEngine;

public class AttackOperation : IAwaitable<AsyncExtensions.Void>
{
    public class AttackOperationAwaiter : AwaiterBase<AsyncExtensions.Void>
    {
        private AttackOperation _attackOperation;

        public AttackOperationAwaiter(AttackOperation attackOperation)
        {
            _attackOperation = attackOperation;
            attackOperation.OnComplete += onComplete;
        }

        private void onComplete()
        {
            _attackOperation.OnComplete -= onComplete;
            onWaitFinish(new AsyncExtensions.Void());
        }
    }

    private event Action OnComplete;

    private readonly UnitAttack _attackCommandExecutor;
    private readonly IAttackable _target;

    private bool _isCancelled;

    public AttackOperation(UnitAttack attackCommandExecutor, IAttackable target)
    {
        _target = target;
        _attackCommandExecutor = attackCommandExecutor;

        var thread = new Thread(attackAlgorythm);
        thread.Start();
    }

    public void Cancel()
    {
        _isCancelled = true;
        OnComplete?.Invoke();
    }

    private void attackAlgorythm(object obj)
    {
        while (true)
        {
            if (
                _attackCommandExecutor == null
                || _attackCommandExecutor.Health == 0
                || _target.Health == 0
                || _isCancelled
                )
            {
                OnComplete?.Invoke();
                return;
            }

            var targetPosition = default(Vector3);
            var ourPosition = default(Vector3);
            var ourRotation = default(Quaternion);
            lock (_attackCommandExecutor)
            {
                targetPosition = _attackCommandExecutor.TargetPosition;
                ourPosition = _attackCommandExecutor.OurPosition;
                ourRotation = _attackCommandExecutor.OurRotation;
            }

            var vector = targetPosition - ourPosition;
            var distanceToTarget = vector.magnitude;
            if (distanceToTarget > _attackCommandExecutor.AttackingDistance)
            {
                var finalDestination = targetPosition - vector.normalized * (_attackCommandExecutor.AttackingDistance * 0.9f);
                _attackCommandExecutor
            .TargetPositions.OnNext(finalDestination);
                Thread.Sleep(100);
            }
            else if (ourRotation != Quaternion.LookRotation(vector))
            {
                _attackCommandExecutor.
            TargetRotations
            .OnNext(Quaternion.LookRotation(vector));
            }
            else
            {
                _attackCommandExecutor.AttackTargets.OnNext(_target);
                Thread.Sleep(_attackCommandExecutor.AttackingPeriod);
            }
        }
    }

    public IAwaiter<AsyncExtensions.Void> GetAwaiter()
    {
        return new AttackOperationAwaiter(this);
    }
}