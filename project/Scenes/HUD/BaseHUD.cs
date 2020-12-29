﻿using Godot;
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

        public virtual void UpdateLabels(float timeLeft, int completedTasks, int totalActions, int correctActions)
        {
            _timeLeftLabel.Text = TimeSpan.FromSeconds(timeLeft).ToString("mm\\:ss");
            _tasksCountLabel.Text = $"{ResourceStrings.CompletedTasksCount} {completedTasks}";
        }

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
        }

        protected void DeleteInstructions()
        {
            foreach (Label item in _instructionsContainer.GetChildren())
            {
                item.QueueFree();
            }
        }
    }
}
