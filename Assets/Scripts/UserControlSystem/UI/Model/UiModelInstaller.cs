using UnityEngine;
using Zenject;

public class UiModelInstaller : MonoInstaller
{
	public override void InstallBindings()
	{
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
		Container.Bind<CommandCreatorBase<ISetGatheringPointCommand>>()
		.To<SetGatheringPointCommandCreator>().AsTransient();

		Container.Bind<CommandButtonsModel>().AsTransient();
		Container.Bind<BottomCenterModel>().AsTransient();

		Container.Bind<float>().WithId(ObjectIdentifiers.Chomper).FromInstance(5.0f);
		Container.Bind<string>().WithId(ObjectIdentifiers.Chomper).FromInstance(ObjectIdentifiers.Chomper);
	}
}