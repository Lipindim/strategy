using UnityEngine;

public interface ISelectable : IHealth
{
	Transform PivotPoint { get; }
	Sprite Icon { get; }
}
