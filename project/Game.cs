using Godot;
using System;
using System.Collections.Generic;

public class Game : Node
{
    private int _score;
    private List<int> _taskNodes;
    private List<int> _clickedNodes;
    private TaskNodesArea _taskNodesArea;
    private SelectedNodesArea _selectedNodesArea;
    private Timer _gameTimer;
    private Timer _taskTimer;
    private Timer _hudUpdateTimer;
    private HUD _hud;

    public bool IsRunning { get; set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _taskNodes = new List<int>();
        _clickedNodes = new List<int>();
        _selectedNodesArea = GetNode<SelectedNodesArea>("SelectedNodesArea");
        _taskNodesArea = GetNode<TaskNodesArea>("TaskNodesArea");
        _hud = GetNode<HUD>("HUD");
        _taskTimer = GetNode<Timer>("TaskTimer");
        _gameTimer = GetNode<Timer>("GameTimer");
        _hudUpdateTimer = GetNode<Timer>("HudUpdateTimer");

        IsRunning = false;

        _hud.Connect("StartGamePressed", this, "StartGame");
        _taskNodesArea.Connect("AddedTaskNode", this, "AddTaskNode");
        _gameTimer.Connect("timeout", this, "StopGame");
        _taskTimer.Connect("timeout", this, "GenerateNewTask");
        _hudUpdateTimer.Connect("timeout", this, "UpdateLabels");
        var nodes = GetTree().GetNodesInGroup("clickableNodes");   
        foreach (ClickableNode node in nodes)
        {
            node.Connect("Clicked", this, "NodeClicked");
        }
    }

    public void NodeClicked(int type)
    {
        if (!IsRunning) return;

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
        _taskTimer.Stop();
        _taskTimer.Start();
    }

    public void StopGame()
    {
        IsRunning = false;
        _hud.ShowGameOverHUD();
        _hud.UpdateLabels(0, 0, _score);
        _gameTimer.Stop();
        _taskTimer.Stop();
    }

    public void StartGame() 
    {
        _score = 0;
        IsRunning = true;
        _hud.HideGameOverHUD();
        _gameTimer.Start();
        _taskTimer.Start();
        _hudUpdateTimer.Start();
        GenerateNewTask();
    }

    public void UpdateLabels()
    {
        _hud.UpdateLabels(_gameTimer.TimeLeft, _taskTimer.TimeLeft, _score);
    }
}
