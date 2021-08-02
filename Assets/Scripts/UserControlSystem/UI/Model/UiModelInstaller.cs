using UnityEngine;
using Zenject;

public class UiModelInstaller : MonoInstaller
{
	[SerializeField] private GameObject _chomperPrefab;
	[SerializeField] private Vector3Value _groundRightClick;
	[SerializeField] private Vector3Value _initialPoint;
	[SerializeField] private SelectableValue _selectedTarget;

	public override void InstallBindings()
	{
		Container.Bind<Vector3Value>().WithId(ObjectIdentifiers.GroundClickPoint).FromInstance(_groundRightClick);
		Container.Bind<Vector3Value>().WithId(ObjectIdentifiers.InitialPoint).FromInstance(_initialPoint);
		Container.Bind<GameObject>().WithId(ObjectIdentifiers.ChomperPrefab).FromInstance(_chomperPrefab);
		Container.Bind<SelectableValue>().WithId(ObjectIdentifiers.SelectedTarget).FromInstance(_selectedTarget);

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