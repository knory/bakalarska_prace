using Godot;
using System;
using System.Linq;

namespace Components
{
    public abstract class Component<T> : Control
    {
        /// <summary>
        /// Number of sequences in which the component was modified and had the correct value.
        /// </summary>
        private int _correctModifiedSequences = 0;
        protected T SelectedValue { get; private set; }
        public T DefaultValue { get; protected set; }

        protected Sprite _background;
        protected VBoxContainer _windowWrapper;
        protected Label _title;

        /// <summary>
        /// Initializes nodes common for all components.
        /// </summary>
        protected void GetCommonNodes()
        {
            _background = GetNode<Sprite>("Background");
            _windowWrapper = GetNode<VBoxContainer>("WindowWrapper");
            _title = _windowWrapper.GetNode<Label>("Title");
        }

        /// <summary>
        /// Sets the provided value as the component's selected value.
        /// </summary>
        /// <param name="newValue">Value to be set as selected value.</param>
        protected virtual void SetValue(T newValue)
        {
            SelectedValue = newValue;
        }

        /// <summary>
        /// Checks whether the component's value has been modified.
        /// </summary>
        /// <returns>true if the value is different from the default value, otherwise false</returns>
        public virtual bool IsModified() 
        {
            return !DefaultValue.Equals(SelectedValue);
        }
        
        /// <summary>
        /// Compares the provided value with the component's selected value.
        /// </summary>
        /// <param name="expectedValue">Value that the component is expected to have.</param>
        /// <returns>true if expectedValue equals the selected value, otherwise false</returns>
        protected virtual bool CheckSelectedValue(T expectedValue)
        {
            return SelectedValue.Equals(expectedValue);
        }

        /// <summary>
        /// Checks whether the component has the same selected value as the one provided.
        /// If correct, increments number of the component's correct modified sequences.
        /// </summary>
        /// <param name="expectedValue">Component's expected value</param>
        /// <returns>true if expectedValue equals the component's selected value, otherwise false</returns>
        public bool IsCorrect(T expectedValue)
        {
            var result = CheckSelectedValue(expectedValue);

            if (result)
            {
                _correctModifiedSequences++;
            }

            return result;
        }

        /// <summary>
        /// Resets the component's state and selected value to default.
        /// </summary>
        public virtual void ResetState()
        {
            SetValue(DefaultValue);
        }

        /// <summary>
        /// Enables control nodes of the component.
        /// </summary>
        public abstract void EnableComponent();

        /// <summary>
        /// Disables control nodes of the component.
        /// </summary>
        public abstract void DisableComponent();

        /// <summary>
        /// Initializes the component's nodes and applies styling and positioning.
        /// </summary>
        protected abstract void SetupView();
    }
}