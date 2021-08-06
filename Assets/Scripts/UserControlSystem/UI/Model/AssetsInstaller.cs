using UnityEngine;
using Zenject;


[CreateAssetMenu(fileName = "AssetsInstaller", menuName = "Installers/AssetsInstaller")]
public class AssetsInstaller : ScriptableObjectInstaller<AssetsInstaller>
{
    [SerializeField] private AssetsContext _legacyContext;
    [SerializeField] private Vector3Value _groundRightClick;
    [SerializeField] private AttackValue _attackTarget;
    [SerializeField] private SelectableValue _selectable;

    public override void InstallBindings()
    {
        Container.Bind<IAwaitable<IAttackable>>()
            .FromInstance(_attackTarget);
        Container.Bind<IAwaitable<Vector3>>()
            .FromInstance(_groundRightClick);
        Container.BindInstances(_legacyContext, _groundRightClick, _attackTarget, _selectable);
    }
}