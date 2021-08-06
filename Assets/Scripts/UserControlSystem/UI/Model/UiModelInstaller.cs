using UnityEngine;
using Zenject;

public class UiModelInstaller : MonoInstaller
{
	[SerializeField] private GameObject _chomperPrefab;
	[SerializeField] private Vector3Value _initialPoint;

	public override void InstallBindings()
	{
		Container.Bind<Vector3Value>().WithId(ObjectIdentifiers.InitialPoint).FromInstance(_initialPoint);
		Container.Bind<GameObject>().WithId(ObjectIdentifiers.ChomperPrefab).FromInstance(_chomperPrefab);

		Container.Bind<CommandCreatorBase<IProduceUnitCommand>>()
		.To<ProduceUnitCommandCreator>().AsTransient();
		Container.Bind<CommandCreatorBase<IAttackCommand>>()
		.To<AttackCommandCreator>().AsTransient();
		Container.Bind<CommandCreatorBase<IMoveCommand>>()
		.To<MoveCommandCreator>().AsTransient();
		Container.Bind<CommandCreatorBase<IPatrolCommand>>()
		.To<PatrolCommandCreator>().AsTransient();
		Container.Bind<CommandCreatorBase<IStopCommand>>()
		.To<StopCommandCreator>().AsTransient();

		Container.Bind<CommandButtonsModel>().AsTransient();
	}
}