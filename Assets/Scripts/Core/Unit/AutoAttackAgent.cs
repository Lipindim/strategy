using UnityEngine;
using UniRx;

public class AutoAttackAgent : MonoBehaviour
{
    [SerializeField] private ChomperCommandsQueue _queue;

    private void Start()
    {
        AutoAttackEvaluator.AutoAttackCommands
            .ObserveOnMainThread()
            .Where(command => command.Attacker == gameObject)
            .Where(command => command.Attacker != null && command.Target != null)
            .Subscribe(command => autoAttack(command.Target))
            .AddTo(this);
    }

    private void autoAttack(GameObject target)
    {
        _queue.Clear();
        _queue.EnqueueCommand(new AutoAttackCommand(target.GetComponent<IAttackable>()));
    }
}