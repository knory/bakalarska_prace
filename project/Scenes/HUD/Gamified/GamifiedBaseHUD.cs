using Godot;
using Project.Scenes.HUD;
using System;
using System.Collections.Generic;
using Utils;

namespace Scenes.HUD.Gamified
{
    public abstract class GamifiedBaseHUD : BaseHUD
    {
        protected static readonly string _resourcesPath = $"{Constants.GamifiedResourcesPath}HUD/";

        public override void _Ready()
        {
            base._Ready();  

            _instructionsBackground.Texture = (Texture)GD.Load($"{_resourcesPath}instructions_background.png");
            _instructionsBackground.RectPosition = new Vector2(1270, -90);

            _tasksCountBackground.Texture = (Texture)GD.Load($"{_resourcesPath}tasks_count_background.png");
            _tasksCountBackground.RectPosition = new Vector2(1650, 166);
            _tasksCountLabel.RectPosition = new Vector2(25, 75);

            _instructionsFont = (DynamicFont)GD.Load($"{_resourcesPath}bebas_neue_instructions.tres");
            _instructionsFont.Size = 22;
            _instructionsColor = new Color("FFFFFF");

            _instructionsContainer.Set("custom_constants/separation", 6);
            _instructionsContainer.RectPosition = new Vector2(120, 100);

            var tasksFont = (DynamicFont)GD.Load($"{_resourcesPath}bebas_neue_tasks.tres");
            tasksFont.Size = 38;
            _tasksCountLabel.AddFontOverride("font", tasksFont);
            _tasksCountLabel.AddColorOverride("font_color", new Color("FFFFFF"));

            _timeLeftBackground.Texture = (Texture)GD.Load($"{_resourcesPath}time_background.png");
            _timeLeftBackground.RectPosition = new Vector2(1070, -10);
            _timeLeftLabel.RectPosition = new Vector2(90, 10);

            var timeLeftFont = (DynamicFont)GD.Load($"{_resourcesPath}bebas_neue_time.tres");
            timeLeftFont.Size = 58;
            _timeLeftLabel.AddFontOverride("font", timeLeftFont);
            _timeLeftLabel.AddColorOverride("font_color", new Color("FFFFFF"));
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
            confirmLabel.Text = ResourceStrings.Gamified.CompleteSequence;
            _instructionsContainer.AddChild(confirmLabel);
        }

        public override void UpdateLabels(float timeLeft, int completedTasksOrPoints, int totalActions, int correctActions)
        {
            _timeLeftLabel.Text = TimeSpan.FromSeconds(timeLeft).ToString("mm\\:ss");
            
            if (!_streakIsShown)
            {
                _tasksCountLabel.Text = $"{ResourceStrings.Gamified.GainedPoints} {completedTasksOrPoints}";
            }
        }
    }
}
