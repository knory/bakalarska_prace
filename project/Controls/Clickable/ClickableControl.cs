using Godot;
using System;
using Utils;

namespace Controls
{
    public class ClickableControl : Godot.TextureButton
    {
        private Texture _deselectedTexture;
        private Texture _selectedTexture;

        public bool IsSelected { get; set; } = false;
        public bool IsActive { get; set; } = false; //TODO zjistit, ejstli je potrebne
        public int Value { get; set; }

        public event EventHandler<SelectedValueEventArgs> Selected;
        public event EventHandler<SelectedValueEventArgs> Deselected;

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

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {

        }

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

        public void Deselect()
        {
            IsSelected = false;
            SetUnselectedTexture();
        }

        public void Select()
        {
            IsSelected = true;
            SetSelectedTexture();
        }

        public void SetSelectedTexture()
        {
            this.TextureNormal = _selectedTexture;
        }

        public void SetUnselectedTexture()
        {
            this.TextureNormal = _deselectedTexture;
        }
    }
}