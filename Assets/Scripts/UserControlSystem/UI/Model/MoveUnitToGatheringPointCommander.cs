using System;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using Zenject;


public class MoveUnitToGatheringPointCommander : MonoBehaviour
{

    [SerializeField] private ProduceUnitCommandExecutor _produceUnitCommandExecutor;
    [SerializeField] private SetGatheringPointCommandExecutor _setGatheringPointCommandExecutor;

    [Inject] private Vector3Value _groundRightClick;
    [Inject] private CommandCreatorBase<IMoveCommand> _moveCommandCreator;

    private void Start()
    {
        _produceUnitCommandExecutor.LastProducedUnitObservable
            .Subscribe(OnUnitProduced)
            .AddTo(this);
    }

    private async void OnUnitProduced(GameObject unit)
    {
        if (unit == null)
            return;

        var unitMoveExecutor = unit.GetComponent<CommandExecutorBase<IMoveCommand>>();
        if (unitMoveExecutor != null)
        {
            _moveCommandCreator.ProcessCommandExecutor(unitMoveExecutor, command => SendMoveCommand(command, unitMoveExecutor));
            await Task.Delay(10);
            _groundRightClick.SetValue(_setGatheringPointCommandExecutor.GatheringPoint);
        }
    }

    private void SendMoveCommand(IMoveCommand command, ICommandExecutor commandExecutor)
    {
        commandExecutor.ExecuteCommand(command);
    }
}

