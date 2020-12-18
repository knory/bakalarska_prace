using Godot;
using System;
using System.Linq;

namespace Components
{
    public abstract class Component<T> : Control
    {
        private int _correctModifiedSequences = 0;
        protected T SelectedValue { get; private set; }
        public T DefaultValue { get; protected set; }

        protected virtual void SetValue(T newValue)
        {
            SelectedValue = newValue;
        }

        public virtual bool IsModified() 
        {
            return !DefaultValue.Equals(SelectedValue);
        }
        
        protected virtual bool CheckSelectedValue(T expectedValue)
        {
            return SelectedValue.Equals(expectedValue);
        }

        public bool IsCorrect(T expectedValue)
        {
            var result = CheckSelectedValue(expectedValue);

            if (result)
            {
                _correctModifiedSequences++;
            }

            return result;
        }

        public virtual void ResetState()
        {
            SetValue(DefaultValue);
        }

        public abstract void EnableComponent();

        public abstract void DisableComponent();
    }
}