using Godot;
using System;
using Utils;

namespace Scenes
{
    public class GameStartOverlay : Control
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

        /// <summary>
        /// Hides the game start overlay.
        /// </summary>
        public void HideOverlay()
        {
            _overlayWrapper.Visible = false;
            _gameCodeValue.MouseFilter = Control.MouseFilterEnum.Ignore;
            _startButton.MouseFilter = Control.MouseFilterEnum.Ignore;
        }

        /// <summary>
        /// Shows the game start overlay.
        /// </summary>
        public void ShowOverlay()
        {
            _overlayWrapper.Visible = true;
            _gameCodeValue.MouseFilter = Control.MouseFilterEnum.Stop;
            _startButton.MouseFilter = Control.MouseFilterEnum.Stop;
        }

        /// <summary>
        /// Hides the game status label.
        /// </summary>
        public void HideGameStatusLabel()
        {
            _gameStatusLabel.Visible = false;
        }

        /// <summary>
        /// Shows the game status label and sets the 'Game Over' text.
        /// </summary>
        public void ShowGameOverLabel()
        {
            ShowGameStatusLabel();
            _gameStatusLabel.Text = "Hra skončila.";
        }

        /// <summary>
        /// Shows the game status label and sets the 'Game Over, results are being sent to the server.'
        /// </summary>
        public void ShowWaitLabel()
        {
            ShowGameStatusLabel();
            _gameStatusLabel.Text = "Hra skončila. Prosím vyčkejte, výsledky se odesílají na server.";
        }

        /// <summary>
        /// Shows the game status label.
        /// </summary>
        public void ShowGameStatusLabel()
        {
            _gameStatusLabel.Visible = true;
        }

        /// <summary>
        /// Shows the error label.
        /// </summary>
        public void ShowErrorLabel()
        {
            _codeErrorLabel.Visible = true;
        }

        /// <summary>
        /// Hides the error label.
        /// </summary>
        public void HideErrorLabel()
        {
            _codeErrorLabel.Visible = false;
        }

        /// <summary>
        /// Starts the game.
        /// </summary>
        private void StartButtonPressed()
        {
            StartGame?.Invoke(this, new GameConfigEventArgs { EncodedConfig = _gameCodeValue.Text, Nickname = _nicknameValue.Text });
        }
    }
}