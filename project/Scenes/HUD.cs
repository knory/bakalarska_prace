using Godot;
using System;
using Utils;

namespace Scenes
{
    public class HUD : CanvasLayer
    {
        private Label _startCountdownLabel;
        private Label _scoreLabel;
        private Label _currentComboLabel;
        private Label _gameTimeLabel;
        private Label _taskTimeLabel;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            var wrapper = GetNode<VBoxContainer>("MarginContainer/VBoxContainer");
            _startCountdownLabel = wrapper.GetNode<Label>("StartCountdownLabel");
            var gameLabelsWrapper = wrapper.GetNode<VBoxContainer>("VBoxContainer");
            _scoreLabel = gameLabelsWrapper.GetNode<Label>("ScoreLabel");
            _currentComboLabel = gameLabelsWrapper.GetNode<Label>("CurrentComboLabel");
            _gameTimeLabel = gameLabelsWrapper.GetNode<Label>("GameTimeLabel");
            _taskTimeLabel = gameLabelsWrapper.GetNode<Label>("TaskTimeLabel");
        }

        public void UpdateCountdownLabel(float countdownTimeLeft)
        {
            _startCountdownLabel.Text = $"{ResourceStrings.GameStartsIn} {new DateTimeOffset().AddSeconds(countdownTimeLeft).ToString("mm:ss")}";
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

        public void HideComboModifierLabel()
        {
            _currentComboLabel.Visible = false;
        }

        public void ShowCountdownLabel()
        {
            _startCountdownLabel.Visible = true;
        }

        public void HideCountdownLabel()
        {
            _startCountdownLabel.Visible = false;
        }

        private void UpdateGeneralLabels(int combo, float gameTimeLeft, float taskTimeLeft)
        {
            _currentComboLabel.Text = $"{ResourceStrings.CurrentCombo}: {combo}";
            _gameTimeLabel.Text = $"{ResourceStrings.TotalTimeLeft}: {new DateTimeOffset().AddSeconds(gameTimeLeft).ToString("mm:ss")}";
            _taskTimeLabel.Text = $"{ResourceStrings.TaskTimeLeft}: {new DateTimeOffset().AddSeconds(taskTimeLeft).ToString("mm:ss")}";
        }
    }
}