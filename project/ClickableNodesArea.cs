using Godot;
using System;

public class ClickableNodesArea : Node2D
{
    private const int NODE_WIDTH = 100;
    private const int NODE_SPACE = 20;
    private const int NODE_TOTAL_WIDTH = NODE_WIDTH + NODE_SPACE;
    private const int NUMBER_OF_NODES = 5;
    private static readonly KeyList[] KEYS = {KeyList.Q, KeyList.W, KeyList.E, KeyList.R, KeyList.T};
    private PackedScene _clickableNodeScene;
    
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        var containerPosition = GetNode<Polygon2D>("Polygon2D").Position;
        _clickableNodeScene = (PackedScene)ResourceLoader.Load("res://ClickableNode.tscn");

        for (int i = 0; i < NUMBER_OF_NODES; i++)
        {
            var newNode = (ClickableNode)_clickableNodeScene.Instance();
            var vector = new Vector2(containerPosition.x + (NODE_TOTAL_WIDTH * i) + NODE_WIDTH / 2, containerPosition.y + NODE_WIDTH / 2);
            newNode.Init(i, i, (int)KEYS[i], vector);
            AddChild(newNode);
            newNode.AddToGroup("clickableNodes");
        }
    }
}
