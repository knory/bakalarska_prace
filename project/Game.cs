using Godot;
using System;
using System.Collections.Generic;

public class Game : Node
{
    private int _score = 0;
    private List<int> _taskNodes;
    private List<int> _clickedNodes;
    private Label _label;
    private TaskNodesArea _taskNodesArea;
    private SelectedNodesArea _selectedNodesArea;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _taskNodes = new List<int>();
        _clickedNodes = new List<int>();
        _selectedNodesArea = GetNode<SelectedNodesArea>("SelectedNodesArea");
        _taskNodesArea = GetNode<TaskNodesArea>("TaskNodesArea");
        _taskNodesArea.Connect("AddedTaskNode", this, "AddTaskNode");
        GenerateNewTask();

        _label = GetNode<Label>("Label");
        _label.Text = _score.ToString();

        var nodes = GetTree().GetNodesInGroup("nodes");   
        foreach (ClickableNode node in nodes)
        {
            node.Connect("Clicked", this, "NodeClicked");
        }
    }

    public void NodeClicked(int type)
    {
        _clickedNodes.Add(type);
        _selectedNodesArea.AddSelectedNode(type);

        if (_clickedNodes.Count == _taskNodes.Count)
        {
            var correctNodes = 0;
            for (int i = 0; i < _clickedNodes.Count; i++)
            {
                if (_clickedNodes[i] == _taskNodes[i])
                {
                    correctNodes++;
                }
            }

            if (correctNodes == _clickedNodes.Count)
            {
                correctNodes += 5;
            }

            _score += correctNodes;
            _label.Text = _score.ToString();
            GenerateNewTask();
        }
    }

    public void AddTaskNode(int type)
    {
        _taskNodes.Add(type);
    }

    public void GenerateNewTask()
    {
        _clickedNodes.Clear();
        _taskNodes.Clear();
        _taskNodesArea.GenerateTaskNodes();
        _selectedNodesArea.DeleteSelectedNodes();
    }
}
