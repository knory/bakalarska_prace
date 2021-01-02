using Godot;
using System;
using Utils;

namespace Scenes
{
    public class GameStartOverlay : CanvasLayer
    {
        private Label _gameStatusLabel;
        private MarginContainer _overlayWrapper;
        private LineEdit _nicknameValue;
        private LineEdit _gameCodeValue;
        private Label _codeErrorLabel;
        private Label _enterNicknameLabel;
        private Label _enterCodeLabel;
        private Button _startButton;

        public event EventHandler<GameConfigEventArgs> StartGame;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            _overlayWrapper = GetNode<MarginContainer>("MarginContainer");
            _gameStatusLabel = _overlayWrapper.GetNode<Label>("GameStatusContainer/GameStatusLabel");
            _nicknameValue = _overlayWrapper.GetNode<LineEdit>("VBoxContainer/NicknameLabel");
            _gameCodeValue = _overlayWrapper.GetNode<LineEdit>("VBoxContainer/GameCodeValue");
            _codeErrorLabel = _overlayWrapper.GetNode<Label>("VBoxContainer/CodeErrorLabel");
            _enterNicknameLabel = _overlayWrapper.GetNode<Label>("VBoxContainer/EnterNicknameLabel");
            _enterCodeLabel = _overlayWrapper.GetNode<Label>("VBoxContainer/EnterCodeLabel");
            _startButton = _overlayWrapper.GetNode<Button>("VBoxContainer/StartButton");
            
            _gameStatusLabel.Visible = false;
            _codeErrorLabel.Visible = false;

            var font = (DynamicFont)GD.Load($"{Constants.ResourcesPath}Fonts/Montserrat/montserrat_regular.tres");
            font.Size = 25;

            _enterNicknameLabel.AddFontOverride("font", font);
            _enterCodeLabel.AddFontOverride("font", font);
            _codeErrorLabel.AddFontOverride("font", font);
            _nicknameValue.AddFontOverride("font", font);
            _gameCodeValue.AddFontOverride("font", font);
            _startButton.AddFontOverride("font", font);

            var boldFont = (DynamicFont)GD.Load($"{Constants.ResourcesPath}Fonts/Montserrat/montserrat_bold.tres");
            boldFont.Size = 55;

            _gameStatusLabel.AddFontOverride("font", boldFont);
            _gameStatusLabel.AddColorOverride("font_color", new Color("#bf5034"));

            _startButton.Connect("pressed", this, nameof(StartButtonPressed));
        }

        public void HideOverlay()
        {
            _overlayWrapper.Visible = false;
            _gameCodeValue.MouseFilter = Control.MouseFilterEnum.Ignore;
            _startButton.MouseFilter = Control.MouseFilterEnum.Ignore;
        }

        public void ShowOverlay()
        {
            _overlayWrapper.Visible = true;
            _gameCodeValue.MouseFilter = Control.MouseFilterEnum.Stop;
            _startButton.MouseFilter = Control.MouseFilterEnum.Stop;
        }

        public void HideGameStatusLabel()
        {
            _gameStatusLabel.Visible = false;
        }

        public void ShowGameOverLabel()
        {
            ShowGameStatusLabel();
            _gameStatusLabel.Text = "Hra skončila.";
        }

        public void ShowWaitLabel()
        {
            ShowGameStatusLabel();
            _gameStatusLabel.Text = "Hra skončila. Prosím vyčkejte, výsledky se odesílají na server.";
        }

        public void ShowGameStatusLabel()
        {
            _gameStatusLabel.Visible = true;
        }

        public void ShowErrorLabel()
        {
            _codeErrorLabel.Visible = true;
        }

        public void HideErrorLabel()
        {
            _codeErrorLabel.Visible = false;
        }

        private void StartButtonPressed()
        {
            StartGame?.Invoke(this, new GameConfigEventArgs { EncodedConfig = _gameCodeValue.Text, Nickname = _nicknameValue.Text });
        }
    }
}