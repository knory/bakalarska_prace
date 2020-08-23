using Godot;
using System;

public class HUD : CanvasLayer
{
    private Label _gameStatusLabel;
    private Label _scoreLabel;
    private Label _currentComboLabel;
    private Label _gameTimeLabel;
    private Label _taskTimeLabel;
    private Button _startButton;

    [Signal]
    public delegate void StartGamePressed();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _gameStatusLabel = GetNode<Label>("GameStatusLabel");
        _scoreLabel = GetNode<Label>("ScoreLabel");
        _currentComboLabel = GetNode<Label>("CurrentComboLabel");
        _gameTimeLabel = GetNode<Label>("GameTimeLabel");
        _taskTimeLabel = GetNode<Label>("TaskTimeLabel");
        _startButton = GetNode<Button>("StartButton");

        _startButton.Connect("pressed", this, "StartButtonPressed");

        _startButton.Text = $"{ResourceStrings.StartGame}";
        _gameStatusLabel.Text = $"{ResourceStrings.GameOver}";
        _gameStatusLabel.Visible = false;
    }

    public void UpdateLabels(int combo, float gameTimeLeft, float taskTimeLeft, int score)
    {
        UpdateGeneralLabels(combo, gameTimeLeft, taskTimeLeft);
        _scoreLabel.Text = $"{ResourceStrings.Score}: {score}";
    }

    public void UpdateLabels(int combo, float gameTimeLeft, float taskTimeLeft, int completedTasks, int perfectTasks)
    {
        UpdateGeneralLabels(combo, gameTimeLeft, taskTimeLeft);
        _scoreLabel.Text = $"{ResourceStrings.PerfectTasks}: {perfectTasks} / {ResourceStrings.CompletedTasks}: {completedTasks}";
    }

    public void ShowGameOverHUD()
    {
        _gameStatusLabel.Visible = true;
        _startButton.Visible = true;
    }

    public void HideGameOverHUD()
    {
        _gameStatusLabel.Visible = false;
        _startButton.Visible = false;
    }

    private void UpdateGeneralLabels(int combo, float gameTimeLeft, float taskTimeLeft)
    {
        _currentComboLabel.Text = $"{ResourceStrings.CurrentCombo}: {combo}";
        _gameTimeLabel.Text = $"{ResourceStrings.TotalTimeLeft}: {new DateTimeOffset().AddSeconds(gameTimeLeft).ToString("mm:ss")}";
        _taskTimeLabel.Text = $"{ResourceStrings.TaskTimeLeft}: {new DateTimeOffset().AddSeconds(taskTimeLeft).ToString("mm:ss")}";
    }

    private void StartButtonPressed()
    {
        EmitSignal("StartGamePressed");
    }
}
