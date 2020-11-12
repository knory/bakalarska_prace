using Godot;
using System;

public class ClickableControl : Godot.TextureButton
{
    private Texture _deselectedTexture;
    private Texture _selectedTexture;

    public bool IsSelected { get; set; } = false;
    public bool IsActive { get; set; } = false;
    public object Value { get; set; }

    public event EventHandler<ClickableControlSelectedEventArgs> Selected;
    public event EventHandler<ClickableControlSelectedEventArgs> Deselected;

    public void Init(Texture deselectedTexture, Texture selectedTexture, object componentValue, bool defaultSelected = false)
    {
        _deselectedTexture = deselectedTexture;
        _selectedTexture = selectedTexture;
        Value = componentValue;
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
        var args = new ClickableControlSelectedEventArgs
        {
            SelectedValue = (int)Value
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
        this.TextureNormal = _deselectedTexture;
    }

    public void Select()
    {
        IsSelected = true;
        this.TextureNormal = _selectedTexture;
    }
}

public class ClickableControlSelectedEventArgs : EventArgs
{
    public int SelectedValue { get; set; }
}