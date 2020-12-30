using Components;
using Controls;
using Godot;
using System;
using Utils;

namespace Components
{
    public abstract class SwitchComponent : Component<bool>
    {
        protected Texture _textureOn;
        protected Texture _textureOff;
        protected MarginContainer _marginContainer;
        protected ClickableControl _clickableControl;

        /// <summary>
        /// Initializes component's value and possible values.
        /// </summary>
        public void Init()
        {
            DefaultValue = false;
            SetValue(DefaultValue);
        }

        public override void _Ready()
        {
            GetCommonNodes();

            _marginContainer = _windowWrapper.GetNode<MarginContainer>("MarginContainer");
            _clickableControl = _marginContainer.GetNode<ClickableControl>("ClickableControl");
            
            SetupView();

            _clickableControl.Init(_textureOff, _textureOn, 0);
            _clickableControl.Selected += OnSelected;
            _clickableControl.Deselected += OnDeselected;
        }

        public override void ResetState()
        {
            _clickableControl.Deselect();
            base.ResetState();
        }

        /// <summary>
        /// Sets the selected value to true.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        public void OnSelected(object sender, EventArgs eventArgs)
        {
            SetValue(true);
        }

        /// <summary>
        /// Sets the selected value to false.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        public void OnDeselected(object sender, EventArgs eventArgs)
        {
            SetValue(false);
        }

        public override void EnableComponent()
        {
            _clickableControl.Disabled = false;
        }

        public override void DisableComponent()
        {
            _clickableControl.Disabled = true;
        }
    }
}