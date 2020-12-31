using Godot;
using Project.Scenes.HUD.Nongamified;
using System;
using Utils;

namespace Scenes.HUD.Nongamified
{
    public class NongamifiedQualityHUD : NongamifiedBaseHUD
    {
        private const int ACTIONS_MARKER_BASE_ROTATION = -75;
        private const int ACTIONS_MARKER_ROTATION_RANGE = 150;
        
        private static readonly string _qualityHudResourcesPath = $"{_resourcesPath}Quality/";

        private TextureRect _totalActionsBackground;
        private RichTextLabel _totalActionsLabel;
        private TextureRect _correctActionsBackground;
        private TextureRect _correctActionsMarker;

        private PackedScene _popupPackedScene;

        public override void _Ready()
        {
            base._Ready();

            _totalActionsBackground = GetNode<TextureRect>("TotalActionsBackground");
            _totalActionsLabel = _totalActionsBackground.GetNode<RichTextLabel>("TotalActionsLabel");
            _correctActionsBackground = GetNode<TextureRect>("CorrectActionsBackground");
            _correctActionsMarker = _correctActionsBackground.GetNode<TextureRect>("CorrectActionsMarker");

            _totalActionsBackground.Texture = (Texture)GD.Load($"{_qualityHudResourcesPath}total_actions_background.png");
            _totalActionsBackground.RectPosition = new Vector2(415, 0);

            _correctActionsBackground.Texture = (Texture)GD.Load($"{_qualityHudResourcesPath}correct_actions_background.png");
            _correctActionsBackground.RectPosition = new Vector2(759, 0);

            _correctActionsMarker.Texture = (Texture)GD.Load($"{_qualityHudResourcesPath}correct_actions_marker.png");
            _correctActionsMarker.RectPivotOffset = new Vector2(10, 49);
            _correctActionsMarker.RectPosition = new Vector2(110, 20);
            _correctActionsMarker.RectRotation = -75;

            var labelFont = (DynamicFont)GD.Load($"{_qualityHudResourcesPath}montserrat_bold.tres");
            labelFont.Size = 24;

            _totalActionsLabel.RectPosition = new Vector2(18, 32);
            _totalActionsLabel.RectMinSize = new Vector2(350, 30);
            _totalActionsLabel.AddFontOverride("normal_font", labelFont);
            _totalActionsLabel.BbcodeEnabled = true;
            _totalActionsLabel.ScrollActive = false;

            _popupPackedScene = (PackedScene)GD.Load($"res://Scenes/HUD/Nongamified/NongamifiedTaskCompletedPopup.tscn");
        }

        public override void UpdateLabels(float timeLeft, int completedTasks, int totalActions, int correctActions)
        {
            base.UpdateLabels(timeLeft, completedTasks, totalActions, correctActions);

            float correctActionsPercent = 0;

            if (totalActions > 0)
            {
                correctActionsPercent = (float)correctActions / totalActions;
            }

            _correctActionsMarker.RectRotation = (ACTIONS_MARKER_ROTATION_RANGE * correctActionsPercent) + ACTIONS_MARKER_BASE_ROTATION;
            _totalActionsLabel.BbcodeText = $"[color=#000000]{ResourceStrings.TotalActions}[/color] [color=#EB5757]{totalActions}[/color]";
        }

        public override void ShowStreakNotification(int streak)
        {
            _streakNotificationTimer.Start();
            _streakIsShown = true;

            _tasksCountLabel.Text = $"{streak}{ResourceStrings.StreakNotification}";
        }

        public void ShowTaskCompletedPopup(EventHandler<EventArgs> eventHandler, int sequenceOrder, double correctActionsInSequence, 
            double correctActionsAverage, double correctActionsStreak, double correctActionsStreakAverage, double taskTimeLeft, 
            double taskTimeLeftAverage)
        {
            var popupInstance = (NongamifiedTaskCompletedPopup)_popupPackedScene.Instance();
            popupInstance.ConfirmButtonHandler += eventHandler;
            AddChild(popupInstance);
            popupInstance.SetPopupData(sequenceOrder, correctActionsInSequence, correctActionsAverage, correctActionsStreak, correctActionsStreakAverage,
                taskTimeLeft, taskTimeLeftAverage);
        }
    }
}
