using Godot;
using System;
using System.Collections.Generic;

public class SelectedNodesArea : Node2D
{
    private const int NODE_WIDTH = 80;
    private const int NODE_SPACE = 15;
    private const int NODE_TOTAL_WIDTH = NODE_WIDTH + NODE_SPACE;
    private int _selectedNodesCount;
    private PackedScene _selectedNodeScene;
    private Vector2 _containerPosition;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
       _selectedNodesCount = 0;
       _selectedNodeScene = (PackedScene)ResourceLoader.Load("res://SelectedNode.tscn");
       _containerPosition = GetNode<Polygon2D>("Polygon2D").Position;
    }
    
    public void AddSelectedNode(int type)
    {
        var newNode = (SelectedNode)_selectedNodeScene.Instance();
        var vector = new Vector2(_containerPosition.x + (NODE_TOTAL_WIDTH * _selectedNodesCount) + NODE_WIDTH / 2, _containerPosition.y + NODE_WIDTH / 2);
        newNode.Init(type, vector);
        AddChild(newNode);
        _selectedNodesCount++;
    }

    public void DeleteSelectedNodes()
    {
        var children = GetChildren();
        foreach (Node node in children)
        {
            if (node is SelectedNode)
            {
                node.QueueFree();
            }
        }

        _selectedNodesCount = 0;
    }
}
