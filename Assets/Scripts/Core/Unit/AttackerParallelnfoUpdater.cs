using UnityEngine;
using Zenject;

public class AttackerParallelnfoUpdater : MonoBehaviour
{
    [SerializeField] private MainUnit _automaticAttacker;
    [SerializeField] private ChomperCommandsQueue _queue;

    private void Update()
    {
        AutoAttackEvaluator.AttackersInfo.AddOrUpdate(
            gameObject
            , new AutoAttackEvaluator.AttackerParallelnfo(_automaticAttacker.VisionRadius, _queue.CurrentCommand)
            , (go, value) =>
            {
                value.VisionRadius = _automaticAttacker.VisionRadius;
                value.CurrentCommand = _queue.CurrentCommand;
                return value;
            });
    }

    private void OnDestroy()
    {
        AutoAttackEvaluator.AttackersInfo.TryRemove(gameObject, out _);
    }
}