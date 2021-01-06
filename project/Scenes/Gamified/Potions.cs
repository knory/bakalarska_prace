using Components;
using Godot;
using System;
using Utils;

namespace Scenes.Gamified
{
    public class Potions : MultipleSelectComponent
    {
        private string _resourcePath = $"{Constants.GamifiedResourcesPath}Potions/";

        public override void _Ready()
        {
            base._Ready();
            SetupView();
            Init();
        }

        protected override void SetupView()
        {
            _textures = new (Texture, Texture)[]
            {
                ((Texture)GD.Load($"{_resourcePath}green_potion_off.png"), (Texture)GD.Load($"{_resourcePath}green_potion_on.png")),
                ((Texture)GD.Load($"{_resourcePath}red_potion_off.png"), (Texture)GD.Load($"{_resourcePath}red_potion_on.png")),
                ((Texture)GD.Load($"{_resourcePath}blue_potion_off.png"), (Texture)GD.Load($"{_resourcePath}blue_potion_on.png"))
            };

            _windowWrapper.RectPosition = new Vector2(5, 130);

            _horizontalContainer.Set("custom_constants/separation", -10);

            _marginContainer.Set("custom_constants/margin_left", 30);

            _background.Texture = (Texture)GD.Load($"{_resourcePath}background.png");
            _background.Position = new Vector2(170, 170);
        }
    }
}
