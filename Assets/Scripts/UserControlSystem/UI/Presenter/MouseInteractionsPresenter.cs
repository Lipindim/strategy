using System.Linq;
using UnityEngine;


public class MouseInteractionsPresenter : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private SelectableValue _selectedObject;

    private Outline _previousOutline;


    private void Update()
    {
        if (!Input.GetMouseButtonUp(0))
        {
            return;
        }
        var hits = Physics.RaycastAll(_camera.ScreenPointToRay(Input.mousePosition));
        if (hits.Length == 0)
        {
            return;
        }
        var selectable = hits
                .Select(hit => hit.collider.GetComponentInParent<ISelectable>())
                .Where(c => c != null)
                .FirstOrDefault();
        _selectedObject.SetValue(selectable);

        RemovePreviousOutline();
        if (selectable != null)
            AddOutline(((MonoBehaviour)selectable).gameObject);
    }

    private void RemovePreviousOutline()
    {
        if (_previousOutline != null)
            _previousOutline.OutlineMode = Outline.Mode.OutlineHidden;
    }

    private void AddOutline(GameObject gameObject)
    {
        var outline = gameObject.GetComponent<Outline>();
        if (outline == null)
        {
            outline = gameObject.AddComponent<Outline>();
            outline.OutlineMode = Outline.Mode.OutlineAll;
            outline.OutlineColor = Color.yellow;
            outline.OutlineWidth = 5f;
            _previousOutline = outline;
        }
        else
        {
            outline.OutlineMode = Outline.Mode.OutlineAll;
        }
    }
}
