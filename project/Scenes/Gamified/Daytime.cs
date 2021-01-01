using Components;
using Godot;
using Models;
using System;
using Utils;

namespace Scenes.Gamified
{
    public class Daytime : ProgressBarComponent
    {
        private string _resourcesPath = $"{Constants.GamifiedResourcesPath}Daytime/";

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            base._Ready();
            SetupView();
            Init();
        }

        protected override void SetupView()
        {
            _background.Texture = (Texture)GD.Load($"{_resourcesPath}background.png");
            _background.Position = new Vector2(300, 30);

            _leftButtonTexture = (Texture)GD.Load($"{_resourcesPath}sun.png");
            _rightButtonTexture = (Texture)GD.Load($"{_resourcesPath}moon.png");

            _progressBarStates = new ProgressBarState[]
            {
                new ProgressBarState { Id = 0, Texture = (Texture)GD.Load($"{_resourcesPath}daytime_1.png") },
                new ProgressBarState { Id = 1, Texture = (Texture)GD.Load($"{_resourcesPath}daytime_2.png") },
                new ProgressBarState { Id = 2, Texture = (Texture)GD.Load($"{_resourcesPath}daytime_3.png") },
            };

            _windowWrapper.RectPosition = new Vector2(70, 5);

            var leftButton = _progressBarSideScrollControl.GetNode<TextureButton>("LeftButton");
            var rightButton = _progressBarSideScrollControl.GetNode<TextureButton>("RightButton");

            leftButton.RectPosition = new Vector2(-55, -18);
            rightButton.RectPosition = new Vector2(420, -18);
        }
    }
}
