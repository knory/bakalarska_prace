using Godot;
using System;

public class HUD : CanvasLayer
{
    private Label _gameStatusLabel;
    private Label _scoreLabel;
    private Label _scoreDescLabel;
    private Label _gameTimeLabel;
    private Label _gameTimeDescLabel;
    private Label _taskTimeLabel;
    private Label _taskTimeDescLabel;
    private Button _startButton;

    [Signal]
    public delegate void StartGamePressed();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _gameStatusLabel = GetNode<Label>("GameStatusLabel");
        _scoreLabel = GetNode<Label>("ScoreLabel");
        _scoreDescLabel = GetNode<Label>("ScoreDescLabel");
        _gameTimeLabel = GetNode<Label>("GameTimeLabel");
        _gameTimeDescLabel = GetNode<Label>("GameTimeDescLabel");
        _taskTimeLabel = GetNode<Label>("TaskTimeLabel");
        _taskTimeDescLabel = GetNode<Label>("TaskTimeDescLabel");
        _startButton = GetNode<Button>("StartButton");

        _startButton.Connect("pressed", this, "StartButtonPressed");

        _gameStatusLabel.Text = "Game Over";
        _gameStatusLabel.Visible = false;
    }

    public void UpdateLabels(float gameTimeLeft, float taskTimeLeft, int score)
    {
        _gameTimeLabel.Text = new DateTimeOffset().AddSeconds(gameTimeLeft).ToString("mm:ss");
        _taskTimeLabel.Text = new DateTimeOffset().AddSeconds(taskTimeLeft).ToString("mm:ss");
        _scoreLabel.Text = score.ToString();
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

    private void StartButtonPressed()
    {
        EmitSignal("StartGamePressed");
    }
}
