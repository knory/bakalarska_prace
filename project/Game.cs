using Godot;
using System;
using System.Collections.Generic;

public class Game : Node
{
    private int _score;
    private int _completedPerfectTasks;
    private int _completedTasks;
    private int _currentModifier;
    private int _currentPerfectStreak;
    private int _currentFailedStreak;
    private List<int> _taskNodes;
    private List<int> _clickedNodes;
    private TaskNodesArea _taskNodesArea;
    private SelectedNodesArea _selectedNodesArea;
    private Timer _gameTimer;
    private Timer _taskTimer;
    private Timer _hudUpdateTimer;
    private HUD _hud;
    private Config _config;

    public bool IsRunning { get; set; }

    public void Init(string encodedConfig) 
    {
        if (string.IsNullOrEmpty(encodedConfig))
        {
            _config = new Config()
            {
                ComboBreakStreak = 1,
                GameControlsType = GameControls.MouseClick,
                ComboStreak = 3,
                MaxComboModifier = 5,
                PerfectTaskBonusPoints = 5,
                SuccessRatingType = SuccessRating.GainedPoints,
                TasksPerGame = 0,
                TimePerGame = 30,
                TimePerTask = 10,
                UnusedTimeGameBonus = 5,
                UnusedTimeTaskBonus = 1
            };
        }
        else
        {
            //TODO: decode the config string and set up the config
        }
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Init(string.Empty);

        _taskNodes = new List<int>();
        _clickedNodes = new List<int>();
        _selectedNodesArea = GetNode<SelectedNodesArea>("SelectedNodesArea");
        _taskNodesArea = GetNode<TaskNodesArea>("TaskNodesArea");
        _hud = GetNode<HUD>("HUD");
        _taskTimer = GetNode<Timer>("TaskTimer");
        _gameTimer = GetNode<Timer>("GameTimer");
        _hudUpdateTimer = GetNode<Timer>("HudUpdateTimer");

        ConfigSetup();
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
            CheckCompletedTask();

            if (_config.TasksPerGame != 0 && _completedTasks == _config.TasksPerGame)
            {
                StopGame();
            }
        }
    }

    public void AddTaskNode(int type)
    {
        _taskNodes.Add(type);
    }

    public void CheckCompletedTask()
    {
        _taskTimer.Stop();
        var gainedScore = CountGainedScore();
        _score += gainedScore * _currentModifier;
        _completedTasks++;
        GenerateNewTask();
    }

    public void GenerateNewTask()
    {
        _clickedNodes.Clear();
        _taskNodes.Clear();
        _taskNodesArea.GenerateTaskNodes();
        _selectedNodesArea.DeleteSelectedNodes();
        _taskTimer.Start();
    }

    public void StopGame()
    {
        IsRunning = false;
        _hud.ShowGameOverHUD();
        UpdateLabels();
        _gameTimer.Stop();
        _taskTimer.Stop();
        
        // if (_config.SuccessRatingType == SuccessRating.GainedPoints) 
        // {
        //     _hud.UpdateLabels(0, 0, _score);
        // }
        // else
        // {
        //     _hud.UpdateLabels(0, 0, _completedTasks, _completedPerfectTasks);
        // }
    }

    public void StartGame() 
    {
        _score = 0;
        _completedTasks = 0;
        _currentModifier = 1;
        _currentPerfectStreak = 0;
        _currentFailedStreak = 0;
        IsRunning = true;
        _hud.HideGameOverHUD();
        _hudUpdateTimer.Start();
        GenerateNewTask();
        _taskTimer.Start();

        if (_config.TimePerGame != 0)
        {
            _gameTimer.Start();
        }
    }

    public void UpdateLabels()
    {
        if (_config.SuccessRatingType == SuccessRating.GainedPoints)
        {
            _hud.UpdateLabels(_currentModifier, _gameTimer.TimeLeft, _taskTimer.TimeLeft, _score);
        }
        else
        {
            _hud.UpdateLabels(_currentModifier, _gameTimer.TimeLeft, _taskTimer.TimeLeft, _completedTasks, _completedPerfectTasks);
        }
    }

    private int CountGainedScore()
    {
        var correctNodes = 0;
        var gainedScore = 0;
        for (int i = 0; i < _clickedNodes.Count; i++)
        {
            if (_clickedNodes[i] == _taskNodes[i])
            {
                correctNodes++;
            }
        }
        gainedScore += correctNodes;

        if (correctNodes == _clickedNodes.Count)
        {
            gainedScore += _config.PerfectTaskBonusPoints;
            gainedScore += Convert.ToInt32(Math.Round(_taskTimer.TimeLeft)) * _config.UnusedTimeTaskBonus;
            _currentPerfectStreak++;
            _completedPerfectTasks++;

            if (_config.ComboStreak == _currentPerfectStreak)
            {
                IncrementComboModifier();
            }
        }
        else
        {
            _currentFailedStreak++;

            if (_config.ComboBreakStreak == _currentFailedStreak)
            {
                DecrementComboModifier();
            }
        }

        return gainedScore;
    }

    private void ConfigSetup()
    {
        _taskTimer.WaitTime = _config.TimePerTask;
        _gameTimer.WaitTime = _config.TimePerGame;
    }

    private void IncrementComboModifier()
    {
        if (_config.MaxComboModifier == 0 || _config.MaxComboModifier > _currentModifier)
        {
            _currentModifier++;
            _currentPerfectStreak = 0;
        }
        
        _currentFailedStreak = 0;
    }

    private void DecrementComboModifier()
    {
        _currentModifier = 1;
        _currentFailedStreak = 0;
        _currentPerfectStreak = 0;
    }
}
