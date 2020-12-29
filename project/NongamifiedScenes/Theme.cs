using Components;
using Godot;
using System;
using Utils;

namespace NongamifiedScenes
{
    public class Theme : SwitchComponent
    {
        private string _resourcesPath = $"{Constants.NongamifiedResourcesPath}Theme/";

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            base._Ready();
        }

        protected override void SetupView()
        {
            var font = (DynamicFont)GD.Load($"{_resourcesPath}montserrat_light.tres");
            font.Size = 24;

            _title.AddFontOverride("font", font);
            _title.Text = "Motiv";

            _windowWrapper.Set("custom_constants/separation", 30);
            _windowWrapper.RectPosition = new Vector2(14, 10);

            _background.Texture = (Texture)GD.Load($"{_resourcesPath}theme_background.png");
            _background.Position = new Vector2(100, 154);

            _textureOff = (Texture)GD.Load($"{_resourcesPath}theme_off.png");
            _textureOn = (Texture)GD.Load($"{_resourcesPath}theme_on.png");

            _marginContainer.Set("custom_constants/margin_left", 57);
        }
    }
}