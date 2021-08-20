using System;
using UniRx;
using UnityEngine;
using Zenject;


public class PatrolCommandCreator : CancellableCommandCreatorBase<IPatrolCommand, Vector3>
{
    private ISelectable _currentSelected;

    public PatrolCommandCreator([Inject] IObservable<ISelectable> selectable)
    {
        selectable.Subscribe(OnSelected);
    }

    private void OnSelected(ISelectable selectable)
    {
        _currentSelected = selectable;
    }

    public override IPatrolCommand CreateCommand(Vector3 argument) => new PatrolCommand( _currentSelected.PivotPoint.position, argument);
}