using UniRx;
using UnityEngine;
using Zenject;


public class UpdateDeferPresenter : MonoBehaviour
{
    [Inject] private BoolValue _deferValue;

    private void Start()
    {
        var activateDeferStream = Observable.EveryUpdate()
            .Where(_ => Input.GetKeyDown(KeyCode.LeftShift))
            .Subscribe(ActivateDefer)
            .AddTo(this);

        var deactivateDeferStream = Observable.EveryUpdate()
            .Where(_ => Input.GetKeyUp(KeyCode.LeftShift))
            .Subscribe(DeactivateDefer)
            .AddTo(this);
    }

    private void ActivateDefer(long value)
    {
        _deferValue.SetValue(true);
    }

    private void DeactivateDefer(long value)
    {
        _deferValue.SetValue(false);
    }
}

