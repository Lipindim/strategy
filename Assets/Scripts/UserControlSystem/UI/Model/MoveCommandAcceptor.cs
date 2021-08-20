using System;
using UnityEngine;
using Zenject;


public class MoveCommandAcceptor : MonoBehaviour
{
    [SerializeField] private Vector3Value _groundRightClick;
    [SerializeField] private SelectableValue _selectableValue;
    [SerializeField] private BoolValue _deferValue;

    [Inject] private CommandButtonsModel _commandButtonsModel;

    private void Start()
    {
        _groundRightClick.ValueChanged += OnGroundRightClick;
    }

    private void OnGroundRightClick(Vector3 position)
    {
        if (_commandButtonsModel.CommandIsPending)
            return;

        var unitGameObject = (MonoBehaviour)_selectableValue.CurrentValue;
        var commandsQueue = unitGameObject.GetComponent<ChomperCommandsQueue>();
        if (commandsQueue != null)
        {
            var moveCommand = new MoveCommand(_groundRightClick.CurrentValue, _deferValue.CurrentValue);
            commandsQueue.EnqueueCommand(moveCommand);
        }
    }
}

