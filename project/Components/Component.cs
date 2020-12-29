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

        protected Sprite _background;
        protected VBoxContainer _windowWrapper;
        protected Label _title;

        protected void GetCommonNodes()
        {
            _background = GetNode<Sprite>("Background");
            _windowWrapper = GetNode<VBoxContainer>("WindowWrapper");
            _title = _windowWrapper.GetNode<Label>("Title");
        }

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

        protected abstract void SetupView();
    }
}