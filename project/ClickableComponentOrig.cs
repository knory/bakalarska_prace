using Godot;
using System;

public class ClickableComponentOrig : Area2D
{
    private CollisionShape2D _collisionShape;
    private Sprite _sprite;
    private Texture _deselectedTexture;
    private Texture _selectedTexture;

    public bool IsSelected { get; set; } = false;
    public object Value { get; set; }

    [Signal]
    delegate void Selected();
    [Signal]
    delegate void Deselected();

    public void Init(Texture deselectedTexture, Texture selectedTexture, object componentValue, bool defaultSelected = false)
    {
        _sprite = GetNode<Sprite>("Sprite");
        _collisionShape = GetNode<CollisionShape2D>("CollisionShape");
        _deselectedTexture = deselectedTexture;
        _selectedTexture = selectedTexture;
        Value = componentValue;
        IsSelected = defaultSelected;

        if (IsSelected)
        {
            _sprite.Texture = selectedTexture;
            _collisionShape.Scale = selectedTexture.GetSize();
        }
        else
        {
            _sprite.Texture = deselectedTexture;
            _collisionShape.Scale = deselectedTexture.GetSize();
        }
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

    public override void _InputEvent(Godot.Object viewport, InputEvent ev, int shapeIdx)
    {
        if (!(ev is InputEventMouseButton clickedEvent)) return;

        if (clickedEvent.Pressed && clickedEvent.ButtonIndex == (int)ButtonList.Left)
        {
            if (!IsSelected)
            {
                _sprite.Texture = _selectedTexture;
                ((RectangleShape2D)_collisionShape.Shape).Extents = _selectedTexture.GetSize();
                IsSelected = true;
                EmitSignal("Selected");
            }
            else
            {
                _sprite.Texture = _deselectedTexture;
                ((RectangleShape2D)_collisionShape.Shape).Extents = _deselectedTexture.GetSize();
                IsSelected = false;
                EmitSignal("Deselected");
            }
        }
    }
}
