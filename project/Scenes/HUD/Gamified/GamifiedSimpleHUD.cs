using Godot;
using System;
using Utils;

namespace Scenes.HUD.Gamified
{
    public class GamifiedSimpleHUD : GamifiedBaseHUD
    {
        private TextureRect _actionsBackground;
        private VBoxContainer _actionsLabelsContainer;
        private RichTextLabel _completedActionsLabel;
        private RichTextLabel _correctActionsLabel;

        public override void _Ready()
        {
            base._Ready();

            _actionsBackground = GetNode<TextureRect>("ActionsBackground");
            _actionsLabelsContainer = _actionsBackground.GetNode<VBoxContainer>("ActionsLabelsContainer");
            _completedActionsLabel = _actionsLabelsContainer.GetNode<RichTextLabel>("CompletedActionsLabel");
            _correctActionsLabel = _actionsLabelsContainer.GetNode<RichTextLabel>("CorrectActionsLabel");

            var labelsFont = (DynamicFont)GD.Load($"{_resourcesPath}bebas_neue_actions.tres");
            labelsFont.Size = 25;

            _completedActionsLabel.AddFontOverride("normal_font", labelsFont);
            _correctActionsLabel.AddFontOverride("normal_font", labelsFont);

            _actionsBackground.Texture = (Texture)GD.Load($"{_resourcesPath}time_background.png");
            _actionsBackground.RectPosition = new Vector2(800, -10);

            _actionsLabelsContainer.RectPosition = new Vector2(70, 15);
            _actionsLabelsContainer.Set("custom_constants/separation", -2);

            _completedActionsLabel.RectMinSize = new Vector2(250, 30);
            _completedActionsLabel.BbcodeEnabled = true;
            _completedActionsLabel.ScrollActive = false;
            _correctActionsLabel.RectMinSize = new Vector2(250, 30);
            _correctActionsLabel.BbcodeEnabled = true;
            _correctActionsLabel.ScrollActive = false;
        }

        public override void UpdateLabels(float timeLeft, int completedTasks, int totalActions, int correctActions)
        {
            base.UpdateLabels(timeLeft, completedTasks, totalActions, correctActions);

            int correctActionsPercent = 0;

            if (totalActions > 0)
            {
                correctActionsPercent = (int)((double)correctActions / totalActions * 100);
            }

            _completedActionsLabel.BbcodeText = $"[color=#FFFFFF]{ResourceStrings.TotalActions}[/color] [color=#FFB300]{totalActions}[/color]";
            _correctActionsLabel.BbcodeText = $"[color=#FFFFFF]{ResourceStrings.CorrectActions}[/color] [color=#FFB300]{correctActionsPercent} %[/color]";
        }
    }
}
