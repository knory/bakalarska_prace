using Godot;
using Project.Scenes.HUD;
using System;
using System.Collections.Generic;
using Utils;

namespace Scenes.HUD.Nongamified
{
    public abstract class NongamifiedBaseHUD : BaseHUD
    {
        protected static readonly string _resourcesPath = $"{Constants.NongamifiedResourcesPath}HUD/";

        public override void _Ready()
        {
            base._Ready();

            _instructionsBackground.Texture = (Texture)GD.Load($"{_resourcesPath}instructions_background.png");
            _instructionsBackground.RectPosition = new Vector2(1290, 0);

            _tasksCountBackground.Texture = (Texture)GD.Load($"{_resourcesPath}tasks_count_background.png");
            _tasksCountBackground.RectPosition = new Vector2(1617, 216);
            _tasksCountLabel.RectPosition = new Vector2(35, 45);

            _instructionsFont = (DynamicFont)GD.Load($"{_resourcesPath}montserrat_medium.tres");
            _instructionsFont.Size = 22;
            _instructionsColor = new Color("000000");

            _instructionsContainer.Set("custom_constants/separation", 10);
            _instructionsContainer.RectPosition = new Vector2(10, 10);

            var tasksFont = (DynamicFont)GD.Load($"{_resourcesPath}montserrat_extra_bold.tres");
            tasksFont.Size = 24;
            _tasksCountLabel.AddFontOverride("font", tasksFont);
            _tasksCountLabel.AddColorOverride("font_color", new Color("EB5757"));

            _timeLeftBackground.Texture = (Texture)GD.Load($"{_resourcesPath}time_background.png");
            _timeLeftBackground.RectPosition = new Vector2(1026, 0);
            _timeLeftLabel.RectPosition = new Vector2(50, 16);

            var timeLeftFont = (DynamicFont)GD.Load($"{_resourcesPath}montserrat_bold.tres");
            timeLeftFont.Size = 48;
            _timeLeftLabel.AddFontOverride("font", timeLeftFont);
            _timeLeftLabel.AddColorOverride("font_color", new Color("000000"));
            _timeLeftLabel.Align = Label.AlignEnum.Center;
        }

        public override void SetInstructions(List<string> instructions)
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
            confirmLabel.Text = ResourceStrings.Nongamified.CompleteSequenceNongamified;
            _instructionsContainer.AddChild(confirmLabel);
        }

        public override void UpdateLabels(float timeLeft, int completedTasksOrPoints, int totalActions, int correctActions)
        {
            _timeLeftLabel.Text = TimeSpan.FromSeconds(timeLeft).ToString("mm\\:ss");
            
            if (!_streakIsShown)
            {
                _tasksCountLabel.Text = $"{ResourceStrings.Nongamified.CompletedTasksCount} {completedTasksOrPoints}";
            }
        }
    }
}
