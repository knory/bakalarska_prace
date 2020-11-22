using Godot;
using System;

namespace Scenes
{
    public class MainMenu : MarginContainer
    {
        private TextureButton _newGameButton;
        private TextureButton _settingsButton;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            _newGameButton = GetNode<TextureButton>("VerticalWrapper/MenuOptions/NewGameButton");
            _settingsButton = GetNode<TextureButton>("VerticalWrapper/MenuOptions/SettingsButton");
            _newGameButton.Connect("pressed", this, "OpenGameScene");
            _settingsButton.Connect("pressed", this, "OpenSettingsScene");
        }
        
        private void OpenGameScene()
        {
            GetTree().ChangeScene("res://Scenes/Game.tscn");
        }

        private void OpenSettingsScene()
        {
            GetTree().ChangeScene("res://Scenes/Settings.tscn");
        }
    }
}