using Components;
using Godot;
using System;
using Utils;

namespace Scenes.Gamified
{
    public class Chest : SwitchComponent
    {
        private string _resourcesPath = $"{Constants.GamifiedResourcesPath}Chest/";

        public override void _Ready()
        {
            _textureOff = (Texture)GD.Load($"{_resourcesPath}chest_off.png");
            _textureOn = (Texture)GD.Load($"{_resourcesPath}chest_on.png");
            base._Ready();

            Init();
        }

        protected override void SetupView()
        {
            _windowWrapper.RectPosition = new Vector2(14, 8);
        }
    }
}
