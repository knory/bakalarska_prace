using Godot;
using System;
using System.Linq;

public class TaskNodesArea : Node2D
{
    private const int NODE_WIDTH = 70;
    private const int NODE_SPACE = 10;
    private const int NODE_TOTAL_WIDTH = NODE_WIDTH + NODE_SPACE;
    private const int NUMBER_OF_NODES = 5;
    private Vector2 _containerPosition;
    private Random _generator;
    private PackedScene _taskNodeScene;

    [Signal]
    public delegate void AddedTaskNode(int type);

    public override void _Ready()
    {
        _containerPosition = GetNode<Polygon2D>("Polygon2D").Position;
        _generator = new Random();
        _taskNodeScene = (PackedScene)ResourceLoader.Load("res://TaskNode.tscn");
    }

    public void GenerateTaskNodes()
    {
        var children = GetChildren();
        foreach (Node node in children)
        {
            if (node is TaskNode)
            {
                node.QueueFree();
            }
        }

        for (int i = 0; i < NUMBER_OF_NODES; i++)
        {
            var newNode = (TaskNode)_taskNodeScene.Instance();
            var vector = new Vector2(_containerPosition.x + NODE_WIDTH / 2, _containerPosition.y + (NODE_TOTAL_WIDTH * i) + NODE_WIDTH / 2);
            var nodeType = _generator.Next(0, 5);
            newNode.Init(nodeType, vector);
            EmitSignal("AddedTaskNode", nodeType);
            AddChild(newNode);
        }
    }
}
