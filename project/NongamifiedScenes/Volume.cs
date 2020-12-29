using Components;
using Godot;
using Models;
using System;
using Utils;

namespace NongamifiedScenes
{
    public class Volume : ProgressBarComponent
    {
        private string _resourcesPath = $"{Constants.NongamifiedResourcesPath}Volume/";

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            base._Ready();
            SetupView();
            Init();
        }

        protected override void SetupView()
        {
            _background.Texture = (Texture)GD.Load($"{_resourcesPath}volume_background.png");
            _background.Position = new Vector2(273, 86);

            _leftButtonTexture = (Texture)GD.Load($"{_resourcesPath}min_volume.png");
            _rightButtonTexture = (Texture)GD.Load($"{_resourcesPath}max_volume.png");

            _progressBarStates = new ProgressBarState[]
            {
                new ProgressBarState { Id = 0, Texture = (Texture)GD.Load($"{_resourcesPath}volume_1.png") },
                new ProgressBarState { Id = 1, Texture = (Texture)GD.Load($"{_resourcesPath}volume_2.png") },
                new ProgressBarState { Id = 2, Texture = (Texture)GD.Load($"{_resourcesPath}volume_3.png") },
            };

            var font = (DynamicFont)GD.Load($"{_resourcesPath}montserrat_light.tres");
            font.Size = 24;

            _title.AddFontOverride("font", font);
            _title.Text = "Zvuk";

            _windowWrapper.Set("custom_constants/separation", 65);
            _windowWrapper.RectPosition = new Vector2(15, 5);

            _marginContainer.Set("custom_constants/margin_left", 55);

            var leftButton = _progressBarSideScrollControl.GetNode<TextureButton>("LeftButton");
            var rightButton = _progressBarSideScrollControl.GetNode<TextureButton>("RightButton");

            leftButton.RectPosition = new Vector2(-35, -7);
            rightButton.RectPosition = new Vector2(400, -15);
        }
    }
}
