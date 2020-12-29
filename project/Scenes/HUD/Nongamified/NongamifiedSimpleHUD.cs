using Godot;
using System;
using Utils;

namespace Scenes.HUD.Nongamified
{
    public class NongamifiedSimpleHUD : NongamifiedBaseHUD
    {
        private static readonly string _simpleHudResourcesPath = $"{_resourcesPath}Simple/";

        private TextureRect _actionsBackground;
        private VBoxContainer _actionsLabelsContainer;
        private RichTextLabel _completedActionsLabel;
        private RichTextLabel _correctActionsLabel;

        private DynamicFont _labelsFont;

        public override void _Ready()
        {
            base._Ready();

            _actionsBackground = GetNode<TextureRect>("ActionsBackground");
            _actionsLabelsContainer = _actionsBackground.GetNode<VBoxContainer>("ActionsLabelsContainer");
            _completedActionsLabel = _actionsLabelsContainer.GetNode<RichTextLabel>("CompletedActionsLabel");
            _correctActionsLabel = _actionsLabelsContainer.GetNode<RichTextLabel>("CorrectActionsLabel");

            _labelsFont = (DynamicFont)GD.Load($"{_simpleHudResourcesPath}montserrat_bold.tres");
            _labelsFont.Size = 24;

            _completedActionsLabel.AddFontOverride("normal_font", _labelsFont);
            _correctActionsLabel.AddFontOverride("normal_font", _labelsFont);

            _actionsBackground.Texture = (Texture)GD.Load($"{_simpleHudResourcesPath}actions_background.png");
            _actionsBackground.RectPosition = new Vector2(650, 0);

            _actionsLabelsContainer.RectPosition = new Vector2(30, 15);

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

            _completedActionsLabel.BbcodeText = $"[color=#000000]{ResourceStrings.TotalActions}[/color] [color=#EB5757]{totalActions}[/color]";
            _correctActionsLabel.BbcodeText = $"[color=#000000]{ResourceStrings.CorrectActions}[/color] [color=#EB5757]{correctActionsPercent} %[/color]";
        }
    }
}
