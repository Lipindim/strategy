using System;
using UnityEngine;

public abstract class BaseChangedValue<T> : ScriptableObject
{
	public T CurrentValue { get; private set; }

	public event Action<T> ValueChanged;

	public void SetValue(T value)
	{
		CurrentValue = value;
		ValueChanged?.Invoke(value);
	}
}

