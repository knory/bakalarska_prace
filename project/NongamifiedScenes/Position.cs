using Components;
using Godot;
using System;
using Utils;

namespace NongamifiedScenes
{
    public class Position : SwitchComponent
    {
        private string _resourcesPath = $"{Constants.NongamifiedResourcesPath}Position/";

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            _textureOff = (Texture)GD.Load($"{_resourcesPath}position_user.png");
            _textureOn = (Texture)GD.Load($"{_resourcesPath}position_admin.png");
            base._Ready();

            Init();
        }

        protected override void SetupView()
        {
            _background.Texture = (Texture)GD.Load($"{_resourcesPath}position_background.png");
            _background.Position = new Vector2(159, 104);

            var font = (DynamicFont)GD.Load($"{_resourcesPath}montserrat_light.tres");
            font.Size = 24;
            _title.Text = "Pozice";
            _title.AddFontOverride("font", font);

            _windowWrapper.Set("custom_constants/separation", 42);
            _windowWrapper.RectPosition = new Vector2(14, 8);

            _marginContainer.Set("custom_constants/margin_left", 37);
        }
    }
}
