using Godot;
using System;

public class TaskNode : Area2D
{
    private Vector2 _position;
    public int Type { get; set; }

    public void Init(int type, Vector2 position)
    {
        Type = type;
        _position = position;
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        var texture = (Texture)GD.Load($"res://{Variables.SpriteNames[Type]}");
        GetNode<Sprite>("Sprite").Texture = texture;
        Position = _position;
    }

}
