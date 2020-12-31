using Godot;
using System;
using System.Collections.Generic;
using Utils;

namespace Project.Scenes.HUD
{
    public abstract class BaseHUD : Node
    {
        protected TextureRect _tasksCountBackground;
        protected Label _tasksCountLabel;
        protected TextureRect _timeLeftBackground;
        protected Label _timeLeftLabel;
        protected TextureRect _instructionsBackground;
        protected VBoxContainer _instructionsContainer;
        
        protected DynamicFont _instructionsFont;
        protected Color _instructionsColor;

        protected Timer _streakNotificationTimer;
        protected bool _streakIsShown = false;

        public override void _Ready()
        {
            _streakNotificationTimer = GetNode<Timer>("StreakNotificationTimer");
            _streakNotificationTimer.WaitTime = Constants.STREAK_NOTIFICATION_TIME;
            _streakNotificationTimer.OneShot = true;
            _streakNotificationTimer.Connect("timeout", this, nameof(HideStreakNotification));
        }

        /// <summary>
        /// Updates all HUD labels' information based on the provided values.
        /// </summary>
        /// <param name="timeLeft">Game time left in seconds</param>
        /// <param name="completedTasks">Number of completed tasks</param>
        /// <param name="totalActions">Number of total done actions</param>
        /// <param name="correctActions">Number of correctly done actions</param>
        public virtual void UpdateLabels(float timeLeft, int completedTasks, int totalActions, int correctActions)
        {
            _timeLeftLabel.Text = TimeSpan.FromSeconds(timeLeft).ToString("mm\\:ss");
            
            if (!_streakIsShown)
            {
                _tasksCountLabel.Text = $"{ResourceStrings.CompletedTasksCount} {completedTasks}";
            }
        }


        /// <summary>
        /// Updates the list of instructions.
        /// </summary>
        /// <param name="instructions">List of instructions to be newly set</param>
        public void SetInstructions(List<string> instructions)
        {
            DeleteInstructions();

            foreach (var item in instructions)
            {
                var label = new Label();
                label.AddFontOverride("font", _instructionsFont);
                label.AddColorOverride("font_color", _instructionsColor);
                label.Autowrap = true;
                label.RectSize = _instructionsBackground.RectSize;
                label.RectMinSize = new Vector2(_instructionsBackground.RectSize.x - 10, 0);
                label.Text = item;
                _instructionsContainer.AddChild(label);
            }

            var confirmLabel = new Label();
            confirmLabel.AddFontOverride("font", _instructionsFont);
            confirmLabel.AddColorOverride("font_color", _instructionsColor);
            confirmLabel.Autowrap = true;
            confirmLabel.RectSize = _instructionsBackground.RectSize;
            confirmLabel.RectMinSize = new Vector2(_instructionsBackground.RectSize.x - 10, 0);
            confirmLabel.Text = ResourceStrings.CompleteSequence;
            _instructionsContainer.AddChild(confirmLabel);
        }

        /// <summary>
        /// Deletes all instructions from the instruction container.
        /// </summary>
        protected void DeleteInstructions()
        {
            foreach (Label item in _instructionsContainer.GetChildren())
            {
                item.QueueFree();
            }
        }

        /// <summary>
        /// Shows perfect task streak notification.
        /// </summary>
        /// <param name="streak">Perfect task streak number</param>
        public virtual void ShowStreakNotification(int streak)
        {
            return;
        }

        /// <summary>
        /// Turns off the flag to show streak notification.
        /// </summary>
        public void HideStreakNotification()
        {
            _streakIsShown = false;
        }
    }
}
