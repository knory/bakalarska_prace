using Godot;
using Newtonsoft.Json;
using System;
using Utils;

namespace Scenes
{
    public class Settings : Control
    {
        private LineEdit _comboStreakValue;
        private LineEdit _maxComboModifierValue;
        private LineEdit _comboBreakStreakValue;
        private LineEdit _perfectTaskBonusPointsValue;
        private LineEdit _unusedTimeTaskBonusValue;
        private LineEdit _unusedTimeGameBonusValue;
        private LineEdit _timePerTaskValue;
        private LineEdit _timePerGameValue;
        private LineEdit _tasksPerGameValue;
        private LineEdit _generatedCodeValue;
        private Label _codeCopiedLabel;
        private Button _generateCodeButton;
        private Button _copyClipboardButton;
        private Button _backButton;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            var valuesContainer = GetNode<VBoxContainer>("MarginContainer/VBoxContainer/ScrollContainer/VBoxContainer");
            _comboStreakValue = valuesContainer.GetNode<LineEdit>("ComboStreakContainer/Value");
            _maxComboModifierValue = valuesContainer.GetNode<LineEdit>("MaxComboModifierContainer/Value");
            _comboBreakStreakValue = valuesContainer.GetNode<LineEdit>("ComboBreakStreakContainer/Value");
            _perfectTaskBonusPointsValue = valuesContainer.GetNode<LineEdit>("PerfectTaskBonusPointsContainer/Value");
            _unusedTimeTaskBonusValue = valuesContainer.GetNode<LineEdit>("UnusedTimeTaskBonusContainer/Value");
            _unusedTimeGameBonusValue = valuesContainer.GetNode<LineEdit>("UnusedTimeGameBonusContainer/Value");
            _timePerTaskValue = valuesContainer.GetNode<LineEdit>("TimePerTaskContainer/Value");
            _timePerGameValue = valuesContainer.GetNode<LineEdit>("TimePerGameContainer/Value");
            _tasksPerGameValue = valuesContainer.GetNode<LineEdit>("TasksPerGameContainer/Value");

            _generatedCodeValue = GetNode<LineEdit>("MarginContainer/VBoxContainer/GeneratedCodeValue");
            _codeCopiedLabel = GetNode<Label>("MarginContainer/VBoxContainer/CodeCopiedLabel");
            _generateCodeButton = GetNode<Button>("MarginContainer/VBoxContainer/HBoxContainer/GenerateCodeButton");
            _copyClipboardButton = GetNode<Button>("MarginContainer/VBoxContainer/HBoxContainer/CopyClipboardButton");
            _backButton = GetNode<Button>("MarginContainer/VBoxContainer/BackButton");

            _generateCodeButton.Connect("pressed", this, "GenerateConfigCode");
            _copyClipboardButton.Connect("pressed", this, "CopyConfigCodeToClipboard");
            _backButton.Connect("pressed", this, "ChangeSceneToMenu");
        }

        private void GenerateConfigCode()
        {
            var config = new Config()
            {
                ComboStreak = int.Parse(_comboStreakValue.Text),
                MaxComboModifier = int.Parse(_maxComboModifierValue.Text),
                ComboBreakStreak = int.Parse(_comboBreakStreakValue.Text),
                PerfectTaskBonusPoints = int.Parse(_perfectTaskBonusPointsValue.Text),
                UnusedTimeTaskBonus = int.Parse(_unusedTimeTaskBonusValue.Text),
                UnusedTimeGameBonus = int.Parse(_unusedTimeGameBonusValue.Text),
                TimePerTask = int.Parse(_timePerTaskValue.Text),
                TimePerGame = int.Parse(_timePerGameValue.Text),
                TasksPerGame = int.Parse(_tasksPerGameValue.Text)
            };

            var serializedConfig = JsonConvert.SerializeObject(config);
            var configBytesArray = System.Text.Encoding.UTF8.GetBytes(serializedConfig);
            var encodedConfig = System.Convert.ToBase64String(configBytesArray);
            _generatedCodeValue.Text = encodedConfig;
            _copyClipboardButton.Disabled = false;
        }

        private void CopyConfigCodeToClipboard()
        {
            OS.Clipboard = _generatedCodeValue.Text;
            _codeCopiedLabel.Text = "Code copied to clipboard!";
        }

        private void ChangeSceneToMenu()
        {
            GetTree().ChangeScene("res://Scenes/MainMenu.tscn");
        }
    }
}