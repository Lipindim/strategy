using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

public class CommandButtonsPresenter : MonoBehaviour
{
    [SerializeField] private SelectableValue _selectable;
    [SerializeField] private CommandButtonsView _view;

    [Inject] private CommandButtonsModel _model;

    private ISelectable _currentSelectable;

    private void Start()
    {
        _view.OnClick += _model.OnCommandButtonClicked;
        _model.CommandSent.Subscribe(_ => _view.UnblockAllInteractions());
        _model.CommandCancel.Subscribe(_  => _view.UnblockAllInteractions());
        _model.CommandAccepted.Subscribe(_view.BlockInteractions);

        _selectable.Subscribe(OnSelected);
    }

    private void OnSelected(ISelectable selectable)
    {
        if (_currentSelectable == selectable)
            return;
        if (_currentSelectable != null)
            _model.OnSelectionChanged();

        _currentSelectable = selectable;

        _view.Clear();
        if (selectable != null)
        {
            var commandExecutors = new List<ICommandExecutor>();
            commandExecutors.AddRange((selectable as Component).GetComponentsInParent<ICommandExecutor>());
            var queue = (selectable as Component).GetComponentInParent<ICommandsQueue>();
            _view.MakeLayout(commandExecutors, queue);
        }
    }
}