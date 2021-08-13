using UnityEngine;

public interface ISelectable : IHealth, IIcon
{
	Transform PivotPoint { get; }
}
