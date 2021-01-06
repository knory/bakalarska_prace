using Components;
using Godot;
using System;
using Utils;

namespace Scenes.Gamified
{
    public class EnergyBooster : SwitchComponent
    {
        private string _resourcesPath = $"{Constants.GamifiedResourcesPath}EnergyBooster/";

        public override void _Ready()
        {
            base._Ready();
        }

        protected override void SetupView()
        {
            _windowWrapper.RectPosition = new Vector2(0, 116);

            _background.Texture = (Texture)GD.Load($"{_resourcesPath}background.png");
            _background.Position = new Vector2(99, 125);

            _textureOff = (Texture)GD.Load($"{_resourcesPath}button_off.png");
            _textureOn = (Texture)GD.Load($"{_resourcesPath}button_on.png");

            _marginContainer.Set("custom_constants/margin_left", 56);
        }
    }
}