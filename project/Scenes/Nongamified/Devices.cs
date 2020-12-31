using Components;
using Godot;
using System;
using Utils;

namespace Scenes.Nongamified
{
    public class Devices : MultipleSelectComponent
    {
        private string _resourcePath = $"{Constants.NongamifiedResourcesPath}Devices/";

        // Called when the node enters the scene tree for the first time.
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
                ((Texture)GD.Load($"{_resourcePath}headphones_off.png"), (Texture)GD.Load($"{_resourcePath}headphones_on.png")),
                ((Texture)GD.Load($"{_resourcePath}microphone_off.png"), (Texture)GD.Load($"{_resourcePath}microphone_on.png")),
                ((Texture)GD.Load($"{_resourcePath}camera_off.png"), (Texture)GD.Load($"{_resourcePath}camera_on.png"))
            };

            var font = (DynamicFont)GD.Load($"{_resourcePath}montserrat_light.tres");
            font.Size = 24;

            _title.AddFontOverride("font", font);
            _title.Text = "Zařízení";

            _windowWrapper.Set("custom_constants/separation", 35);
            _windowWrapper.RectPosition = new Vector2(14, 8);

            _horizontalContainer.Set("custom_constants/separation", 138);

            _marginContainer.Set("custom_constants/margin_left", 25);

            _background.Texture = (Texture)GD.Load($"{_resourcePath}devices_background.png");
            _background.Position = new Vector2(310, 114);
        }
    }
}
