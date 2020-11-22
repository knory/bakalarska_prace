using Godot;
using System;
using System.Linq;

namespace Components
{
    public abstract class Component : Node2D
    {
        private object[] _possibleValues;
        private object _defaultValue;

        protected object SelectedValue { get; set; }

        protected virtual void SetPossibleValues(object[] possibleValues, object defaultValue)
        {
            _possibleValues = possibleValues;
            _defaultValue = defaultValue;
            SelectedValue = defaultValue;
        }
        
        protected virtual void SetValue(object newValue)
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
}