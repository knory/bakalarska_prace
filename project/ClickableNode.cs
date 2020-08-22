using Godot;
using System;

public class ClickableNode : Area2D
{
    private int _assignedKey;
    private int _order;
    private Vector2 _position;
    public int Type { get; set; }

    [Signal]
    public delegate void Clicked(int type);

    public void Init(int type, int order, int assignedKey, Vector2 position)
    {
        Type = type;
        _order = order;
        _assignedKey = assignedKey;
        _position = position;
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        var texture = (Texture)GD.Load($"res://{Variables.SpriteNames[Type]}");
        GetNode<Sprite>("Sprite").Texture = texture;
        Position = _position;
    }
    
    public override void _Input(InputEvent ev)
    {
        if (!(ev is InputEventKey keyEvent)) return;

        if (keyEvent.Pressed && keyEvent.Scancode == _assignedKey)
        {
            EmitSignal("Clicked", Type);
        }
    }

    public override void _InputEvent(Godot.Object viewport, InputEvent ev, int shapeIdx)
    {
        if (!(ev is InputEventMouseButton clickedEvent)) return;

        if (clickedEvent.Pressed && clickedEvent.ButtonIndex == (int)ButtonList.Left)
        {
            EmitSignal("Clicked", Type);
        }
    }
}
