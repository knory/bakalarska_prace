using Controls;
using Godot;
using System;
using Utils;

namespace Controls
{
    public class ButtonWithBackgroundControl : Control
    {
        private Texture _backgroundTexture;
        private Texture _addButtonTexture;
        private Texture _removeButtonTexture;
        private int _value;
        private EventHandler<SelectedValueEventArgs> _selectedHandler;
        private EventHandler<SelectedValueEventArgs> _deselectedHandler;
        private bool IsSelected;

        protected Sprite _background;
        protected ClickableControl _clickableControl;

        public override void _Ready()
        {
            _background = GetNode<Sprite>("Background");
            _clickableControl = GetNode<ClickableControl>("ClickableControl");
            
            _background.Texture = _backgroundTexture;

            _clickableControl.Init(_addButtonTexture, _removeButtonTexture, _value, IsSelected);
            _clickableControl.Selected += _selectedHandler;
            _clickableControl.Deselected += _deselectedHandler;
        }

        /// <summary>
        /// Initializes the control with provided values.
        /// </summary>
        /// <param name="addButtonTexture">Texture of deactivated button</param>
        /// <param name="removeButtonTexture">Texture of activated button</param>
        /// <param name="value">Value of the control</param>
        /// <param name="selectedHandler">Handler of the Selected event</param>
        /// <param name="deselectedHandler">Handler of the Deselected event</param>
        /// <param name="background">Background texture</param>
        public void Init(Texture addButtonTexture, Texture removeButtonTexture, int value, EventHandler<SelectedValueEventArgs> selectedHandler,
            EventHandler<SelectedValueEventArgs> deselectedHandler, Texture background)
        {
            _addButtonTexture = addButtonTexture;
            _removeButtonTexture = removeButtonTexture;
            _value = value;
            _selectedHandler = selectedHandler;
            _deselectedHandler = deselectedHandler;
            _backgroundTexture = background;
        }

        /// <summary>
        /// Deselects the control.
        /// </summary>
        public void Deselect()
        {
            IsSelected = false;
            _clickableControl?.Deselect();
        }

        /// <summary>
        /// Enables the control.
        /// </summary>
        public void EnableControl()
        {
            _clickableControl.Disabled = false;
        }

        /// <summary>
        /// Disables the control.
        /// </summary>
        public void DisableControl()
        {
            _clickableControl.Disabled = true;
        }
    }
}
