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
            _tasksCountBackground = GetNode<TextureRect>("TasksCountBackground");
            _tasksCountLabel = _tasksCountBackground.GetNode<Label>("TasksCountLabel");
            _timeLeftBackground = GetNode<TextureRect>("TimeLeftBackground");
            _timeLeftLabel = _timeLeftBackground.GetNode<Label>("TimeLeftLabel");
            _instructionsBackground = GetNode<TextureRect>("InstructionsBackground");
            _instructionsContainer = _instructionsBackground.GetNode<VBoxContainer>("InstructionsContainer");

            _streakNotificationTimer = GetNode<Timer>("StreakNotificationTimer");
            _streakNotificationTimer.WaitTime = Constants.STREAK_NOTIFICATION_TIME;
            _streakNotificationTimer.OneShot = true;
            _streakNotificationTimer.Connect("timeout", this, nameof(HideStreakNotification));
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

        /// <summary>
        /// Updates the list of instructions.
        /// </summary>
        /// <param name="instructions">List of instructions to be newly set</param>
        public abstract void SetInstructions(List<string> instructions);

        /// <summary>
        /// Updates all HUD labels' information based on the provided values.
        /// </summary>
        /// <param name="timeLeft">Game time left in seconds</param>
        /// <param name="completedTasksOrPoints">Number of completed tasks or gained points</param>
        /// <param name="totalActions">Number of total done actions</param>
        /// <param name="correctActions">Number of correctly done actions</param>
        public abstract void UpdateLabels(float timeLeft, int completedTasksOrPoints, int totalActions, int correctActions);
    }
}
