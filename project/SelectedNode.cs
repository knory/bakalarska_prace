using Godot;
using System;

public class SelectedNode : Area2D
{
    public void Init (int type, Vector2 position) {
        Position = position;
        var texture = (Texture)GD.Load($"res://{Constants.SpriteNames[type]}");
        GetNode<Sprite>("Sprite").Texture = texture;
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }
}
