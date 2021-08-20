using System;
using UniRx;
using UnityEngine;
using Zenject;


public class UpdateProduceUnitCommandPresenter : MonoBehaviour
{
    [Inject] private IObservable<ISelectable> _selectableValue;
    [Inject] private ProduceUnitCommandValue _produceUnitCommandValue;

    private void Start()
    {
        _selectableValue.Subscribe(UpdateSelectedProduceUnitCommand).AddTo(this);
    }

    public void UpdateSelectedProduceUnitCommand(ISelectable selectable)
    {
        if (selectable == null)
            return;
        var produceUnitCommandExecutor = ((MonoBehaviour)selectable).GetComponent<ProduceUnitCommandExecutor>();
        if (produceUnitCommandExecutor != null)
            _produceUnitCommandValue.SetValue(produceUnitCommandExecutor.ProduceUnitCommand);
    }
}

