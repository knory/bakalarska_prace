using Godot;
using System;
using System.Linq;

namespace Components
{
    public abstract class Component<T> : Node2D
    {
        public T DefaultValue { get; protected set; }
        protected T SelectedValue { get; private set; }

        protected virtual void SetValue(T newValue)
        {
            SelectedValue = newValue;
        }

        //TODO override where equals doesnt work (HashSet...)
        //TODO when HashSet etc, check if Default Value gets changed, when the SelectedValue gets modified
        public virtual bool IsModified() 
        {
            return !DefaultValue.Equals(SelectedValue);
        }
        
        public virtual bool CheckSelectedValue(T expectedValue)
        {
            return SelectedValue.Equals(expectedValue);
        }

        public virtual void ResetState()
        {
            SelectedValue = DefaultValue;
        }
    }
}