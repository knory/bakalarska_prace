using Godot;
using System;
using Utils;

namespace Scenes
{
    public class GameStartOverlay : CanvasLayer
    {
        private Label _gameStatusLabel;
        private MarginContainer _overlayWrapper;
        private LineEdit _gameCodeValue;
        private Label _codeErrorLabel;
        private Button _startButton;
        private Button _backButton;

        public event EventHandler<GameConfigEventArgs> StartGame;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            _overlayWrapper = GetNode<MarginContainer>("MarginContainer");
            _gameStatusLabel = _overlayWrapper.GetNode<Label>("GameStatusContainer/GameStatusLabel");
            _gameCodeValue = _overlayWrapper.GetNode<LineEdit>("VBoxContainer/GameCodeValue");
            _codeErrorLabel = _overlayWrapper.GetNode<Label>("VBoxContainer/CodeErrorLabel");
            _startButton = _overlayWrapper.GetNode<Button>("VBoxContainer/StartButton");
            _backButton = _overlayWrapper.GetNode<Button>("VBoxContainer/BackButton");
            
            _gameStatusLabel.Text = $"Game Over";
            _gameStatusLabel.Visible = false;

            _codeErrorLabel.Visible = false;

            _startButton.Connect("pressed", this, nameof(StartButtonPressed));
        }

        public void HideOverlay()
        {
            _overlayWrapper.Visible = false;
            _gameCodeValue.MouseFilter = Control.MouseFilterEnum.Ignore;
            _startButton.MouseFilter = Control.MouseFilterEnum.Ignore;
            _backButton.MouseFilter = Control.MouseFilterEnum.Ignore;
        }

        public void ShowOverlay()
        {
            _overlayWrapper.Visible = true;
            _gameCodeValue.MouseFilter = Control.MouseFilterEnum.Stop;
            _startButton.MouseFilter = Control.MouseFilterEnum.Stop;
            _backButton.MouseFilter = Control.MouseFilterEnum.Stop;
        }

        public void HideGameStatusLabel()
        {
            _gameStatusLabel.Visible = false;
        }

        public void ShowGameStatusLabel()
        {
            _gameStatusLabel.Visible = true;
        }

        public void ShowCodeErrorLabel()
        {
            _codeErrorLabel.Visible = true;
        }

        public void HideCodeErrorLabel()
        {
            _codeErrorLabel.Visible = false;
        }

        private void StartButtonPressed()
        {
            StartGame?.Invoke(this, new GameConfigEventArgs { EncodedConfig = _gameCodeValue.Text });
        }
    }
}