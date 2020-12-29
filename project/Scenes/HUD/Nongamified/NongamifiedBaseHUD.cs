using Godot;
using Project.Scenes.HUD;
using System;
using Utils;

namespace Scenes.HUD.Nongamified
{
    public abstract class NongamifiedBaseHUD : BaseHUD
    {
        protected static readonly string _resourcesPath = $"{Constants.NongamifiedResourcesPath}HUD/";

        public override void _Ready()
        {
            _tasksCountBackground = GetNode<TextureRect>("TasksCountBackground");
            _tasksCountLabel = _tasksCountBackground.GetNode<Label>("TasksCountLabel");
            _timeLeftBackground = GetNode<TextureRect>("TimeLeftBackground");
            _timeLeftLabel = _timeLeftBackground.GetNode<Label>("TimeLeftLabel");
            _instructionsBackground = GetNode<TextureRect>("InstructionsBackground");
            _instructionsContainer = _instructionsBackground.GetNode<VBoxContainer>("InstructionsContainer");

            _instructionsBackground.Texture = (Texture)GD.Load($"{Constants.NongamifiedResourcesPath}instructions_background.png");
            _instructionsBackground.RectPosition = new Vector2(1290, 0);

            _tasksCountBackground.Texture = (Texture)GD.Load($"{_resourcesPath}tasks_count_background.png");
            _tasksCountBackground.RectPosition = new Vector2(1617, 216);
            _tasksCountLabel.RectPosition = new Vector2(35, 45);

            _instructionsFont = (DynamicFont)GD.Load($"{_resourcesPath}montserrat_medium.tres");
            _instructionsFont.Size = 22;
            _instructionsColor = new Color("000000");

            _instructionsContainer.Set("custom_constants/separation", 10);
            _instructionsContainer.RectPosition = new Vector2(10, 30);

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
    }
}
