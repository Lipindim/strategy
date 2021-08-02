using UnityEngine;


public class InitialPointPresenter : MonoBehaviour
{
    [SerializeField] private SelectableValue _selectableValue;
    [SerializeField] private Vector3Value _initialPoint;

    private void Start()
    {
        _selectableValue.ValueChanged += SetInitialPoint;
    }

    private void SetInitialPoint(ISelectable selected)
    {
        if (selected == null)
        {
            _initialPoint.SetValue(Vector3.zero);
            return;
        }
        Vector3 initialPoint = ((MonoBehaviour)selected).transform.position;
        _initialPoint.SetValue(initialPoint);
    }

    private void OnDestroy()
    {
        _selectableValue.ValueChanged -= SetInitialPoint;
    }
}