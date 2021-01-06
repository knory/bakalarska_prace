using Components;
using Godot;
using System;
using Utils;

namespace Scenes.Gamified
{
    public class Candles : RatingComponent
    {
        private string _resourcesPath = $"{Constants.GamifiedResourcesPath}Candles/";

        public override void _Ready()
        {
            base._Ready();
            Init(Constants.RATING_NUMBER_OF_ITEMS);
        }

        protected override void SetupView()
        {
            _textureOff = (Texture)GD.Load($"{_resourcesPath}candle_off.png");
            _textureOn = (Texture)GD.Load($"{_resourcesPath}candle_on.png");

            _background.Texture = (Texture)GD.Load($"{_resourcesPath}background.png");
            _background.Position = new Vector2(224, 134);

            _windowWrapper.RectPosition = new Vector2(105, -15);

            _horizontalContainer.Set("custom_constants/separation", 15);
        }
    }
}