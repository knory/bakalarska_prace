using Godot;
using System;
using System.Linq;

public abstract class Component : Node2D, IComponent
{
    private object[] _possibleValues;
    private object _defaultValue;

    public object SelectedValue { get; set; }

	public virtual void SetPossibleValues(object[] possibleValues, object defaultValue)
    {
        _possibleValues = possibleValues;
        _defaultValue = defaultValue;
        SelectedValue = defaultValue;
    }
	
	public virtual void SetValue(object newValue)
    {
        if (_possibleValues.Contains(newValue))
        {
            SelectedValue = newValue;
        }
        else
        {
            throw new ArgumentOutOfRangeException(nameof(newValue), newValue, $"The value of {nameof(newValue)} was expected to be in range of {string.Join(", ", _possibleValues)}.");
        }
    }
	
	public virtual bool CheckSelectedValue(object expectedValue = null)
    {
        if (_possibleValues?.Contains(expectedValue) ?? false)
        {
            return SelectedValue == expectedValue;
        }
        else
        {
            return SelectedValue == _defaultValue;
        }
    }

    public virtual void ResetState()
    {
        SelectedValue = _defaultValue;
    }
}