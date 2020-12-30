using Godot;
using System;
using Utils;

namespace Controls
{
    public class ClickableControl : TextureButton
    {
        private Texture _deselectedTexture;
        private Texture _selectedTexture;

        public bool IsSelected { get; set; } = false;
        public bool IsActive { get; set; } = false;
        public int Value { get; set; }

        public event EventHandler<SelectedValueEventArgs> Selected;
        public event EventHandler<SelectedValueEventArgs> Deselected;

        /// <summary>
        /// Initializes the control with provided values.
        /// </summary>
        /// <param name="deselectedTexture">Texture of deactivated control</param>
        /// <param name="selectedTexture">Texture of activated control</param>
        /// <param name="controlValue">Value of the control</param>
        /// <param name="defaultSelected">Sets whether the control is activated by default</param>
        public void Init(Texture deselectedTexture, Texture selectedTexture, int controlValue, bool defaultSelected = false)
        {
            _deselectedTexture = deselectedTexture;
            _selectedTexture = selectedTexture;
            Value = controlValue;
            IsSelected = defaultSelected;

            if (IsSelected)
            {
                this.TextureNormal = selectedTexture;
            }
            else
            {
                this.TextureNormal = deselectedTexture;
            }

            this.Connect("pressed", this, nameof(HandleClick));
        }

        public override void _Ready()
        { }

        /// <summary>
        /// Handles the pressed event. Changes the control's texture and inverts value of IsSelected.
        /// 
        /// Invokes Selected or Deselected event handler.
        /// </summary>
        private void HandleClick() {
            var args = new SelectedValueEventArgs
            {
                SelectedValue = Value
            };
            
            if (!IsSelected)
            {
                this.TextureNormal = _selectedTexture;
                IsSelected = true;

                Selected?.Invoke(this, args);
            }
            else
            {
                this.TextureNormal = _deselectedTexture;
                IsSelected = false;

                Deselected?.Invoke(this, args);
            }
        }

        /// <summary>
        /// Deselects the control.
        /// </summary>
        public void Deselect()
        {
            IsSelected = false;
            SetUnselectedTexture();
        }

        /// <summary>
        /// Selects the control.
        /// </summary>
        public void Select()
        {
            IsSelected = true;
            SetSelectedTexture();
        }

        /// <summary>
        /// Sets the control's texture to selected.
        /// </summary>
        public void SetSelectedTexture()
        {
            this.TextureNormal = _selectedTexture;
        }

        /// <summary>
        /// Sets the control's texture to deselected.
        /// </summary>
        public void SetUnselectedTexture()
        {
            this.TextureNormal = _deselectedTexture;
        }
    }
}