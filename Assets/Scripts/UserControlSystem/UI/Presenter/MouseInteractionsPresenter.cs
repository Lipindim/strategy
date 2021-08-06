using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseInteractionsPresenter : MonoBehaviour
{
    [SerializeField] private EventSystem _eventSystem;
    [SerializeField] private Camera _camera;
    [SerializeField] private SelectableValue _selectedObject;
    [SerializeField] private SelectableValue _selectedTarget;
    [SerializeField] private AttackValue _attackValue;

    [SerializeField] private Vector3Value _groundClicksRMB;
    [SerializeField] private Transform _groundTransform;

    private Plane _groundPlane;

    private void Start()
    {
        _groundPlane = new Plane(_groundTransform.up, 0);
    }

    private void Update()
    {
        if (!Input.GetMouseButtonUp(0) && !Input.GetMouseButton(1))
            return;
        if (_eventSystem.IsPointerOverGameObject())
            return;

        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonUp(0))
        {
            _selectedObject.SetValue(GetSelectedValue<ISelectable>(ray));
        }
        else
        {
            if (_groundPlane.Raycast(ray, out var enter))
                _groundClicksRMB.SetValue(ray.origin + ray.direction * enter);
            _attackValue.SetValue(GetSelectedValue<IAttackable>(ray));
        }
    }

    private T GetSelectedValue<T>(Ray ray) where T : class
    {
        var hits = Physics.RaycastAll(ray);
        if (hits.Length == 0)
            return null;
        var selectable = hits
            .Select(hit => hit.collider.GetComponentInParent<T>())
            .Where(c => c != null)
            .FirstOrDefault();
        return selectable;
    }
}