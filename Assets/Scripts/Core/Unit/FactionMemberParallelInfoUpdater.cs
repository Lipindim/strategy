using UnityEngine;


public class FactionMemberParallelInfoUpdater : MonoBehaviour
{
    [SerializeField] private FactionMember _factionMember;

    public void Update()
    {
        AutoAttackEvaluator.FactionMembersInfo.AddOrUpdate(
            gameObject
            , new AutoAttackEvaluator.FactionMemberParallelInfo(transform.position, _factionMember.FactionId)
            , (go, value) =>
            {
                value.Position = transform.position;
                value.Faction = _factionMember.FactionId;
                return value;
            });
    }

    private void OnDestroy()
    {
        AutoAttackEvaluator.FactionMembersInfo.TryRemove(gameObject, out _);
    }
}