using UnityEngine;


public class OutlineSelectorPresenter : MonoBehaviour
{
    [SerializeField] private SelectableValue _selectable;

    private OutlineSelector[] _outlineSelectors;
    private ISelectable _currentSelectable;

    private void Start()
    {
        _selectable.ValueChanged += OnSelected;
        OnSelected(_selectable.CurrentValue);
    }

    private void OnSelected(ISelectable selectable)
    {
        if (_currentSelectable == selectable)
            return;

        _currentSelectable = selectable;

        SetSelected(_outlineSelectors, false);
        _outlineSelectors = null;

        if (selectable != null)
        {
            _outlineSelectors = (selectable as Component).GetComponentsInParent<OutlineSelector>();
            SetSelected(_outlineSelectors, true);
        }

        static void SetSelected(OutlineSelector[] selectors, bool value)
        {
            if (selectors != null)
            {
                for (int i = 0; i < selectors.Length; i++)
                    selectors[i].SetSelected(value);
            }
        }
    }
}
