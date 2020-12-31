using Components;
using Godot;
using System;
using Utils;

namespace Scenes.Nongamified
{
    public class Priority : SingleSelectComponent
    {
        private string _resourcesPath = $"{Constants.NongamifiedResourcesPath}Priority/";

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
                ((Texture)GD.Load($"{_resourcesPath}low_off.png"), (Texture)GD.Load($"{_resourcesPath}low_on.png")),
                ((Texture)GD.Load($"{_resourcesPath}medium_off.png"), (Texture)GD.Load($"{_resourcesPath}medium_on.png")),
                ((Texture)GD.Load($"{_resourcesPath}high_off.png"), (Texture)GD.Load($"{_resourcesPath}high_on.png"))
            };

            _background.Texture = (Texture)GD.Load($"{_resourcesPath}priority_background.png");
            _background.Position = new Vector2(199, 114);

            _windowWrapper.RectPosition = new Vector2(20, 10);
            _windowWrapper.Set("custom_constants/separation", 55);

            _horizontalContainer.Set("custom_constants/separation", 105);

            _marginContainer.Set("custom_constants/margin_left", 15);

            var font = (DynamicFont)GD.Load($"{_resourcesPath}montserrat_light.tres");
            font.Size = 24;

            _title.AddFontOverride("font", font);
            _title.Text = "Priorita";
        }
    }
}
