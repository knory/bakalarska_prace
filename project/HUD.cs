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
        var wrapper = GetNode<VBoxContainer>("MarginContainer/VBoxContainer");
        _gameStatusLabel = wrapper.GetNode<Label>("GameStatusLabel");
        _startButton = wrapper.GetNode<Button>("StartButton");
        var gameLabelsWrapper = wrapper.GetNode<VBoxContainer>("VBoxContainer");
        _scoreLabel = gameLabelsWrapper.GetNode<Label>("ScoreLabel");
        _currentComboLabel = gameLabelsWrapper.GetNode<Label>("CurrentComboLabel");
        _gameTimeLabel = gameLabelsWrapper.GetNode<Label>("GameTimeLabel");
        _taskTimeLabel = gameLabelsWrapper.GetNode<Label>("TaskTimeLabel");

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

    public void HideComboModifierLabel()
    {
        _currentComboLabel.Visible = false;
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
