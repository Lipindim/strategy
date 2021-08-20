using System;
using UnityEngine;
using Zenject;


[CreateAssetMenu(fileName = "AssetsInstaller", menuName = "Installers/AssetsInstaller")]
public class AssetsInstaller : ScriptableObjectInstaller<AssetsInstaller>
{
    [SerializeField] private AssetsContext _legacyContext;
    [SerializeField] private Vector3Value _groundRightClick;
    [SerializeField] private AttackValue _attackTarget;
    [SerializeField] private SelectableValue _selectable;
    [SerializeField] private Sprite _chomperSprite;
    [SerializeField] private GameObject _chomperPrefab;
    [SerializeField] private BoolValue _deferValue;
    [SerializeField] private ProduceUnitCommandValue _produceUnitCommandValue;

    public override void InstallBindings()
    {
        Container.Bind<IAwaitable<IAttackable>>()
            .FromInstance(_attackTarget);
        Container.Bind<IAwaitable<Vector3>>()
            .FromInstance(_groundRightClick);
        Container.BindInstances(_legacyContext, _groundRightClick, _attackTarget);
        Container.Bind<Sprite>().WithId(ObjectIdentifiers.Chomper).FromInstance(_chomperSprite);
        Container.Bind<GameObject>().WithId(ObjectIdentifiers.Chomper).FromInstance(_chomperPrefab);
        Container.Bind<IObservable<ISelectable>>().FromInstance(_selectable);
        Container.Bind<BoolValue>().FromInstance(_deferValue);
        Container.Bind<ProduceUnitCommandValue>().FromInstance(_produceUnitCommandValue);
    }
}