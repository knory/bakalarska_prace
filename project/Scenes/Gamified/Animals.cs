using Components;
using Controls;
using Godot;
using System;
using Utils;

namespace Scenes.Gamified
{
    public class Animals : SingleSelectComponent
    {
        private string _resourcesPath = $"{Constants.GamifiedResourcesPath}Animals/";

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            base._Ready();
            SetupView();
            Init();

            //foreach (var item in _horizontalContainer.GetChildren())
            //{
            //    if (!(item is ClickableControl control)) continue;

            //    control.RectMinSize = new Vector2(50, 0);
            //    control.RectSize = new Vector2(50, 0);
            //}
        }

        protected override void SetupView()
        {
            _textures = new (Texture, Texture)[]
            {
                ((Texture)GD.Load($"{_resourcesPath}elephant_off.png"), (Texture)GD.Load($"{_resourcesPath}elephant_on.png")),
                ((Texture)GD.Load($"{_resourcesPath}camel_off.png"), (Texture)GD.Load($"{_resourcesPath}camel_on.png")),
                ((Texture)GD.Load($"{_resourcesPath}donkey_off.png"), (Texture)GD.Load($"{_resourcesPath}donkey_on.png"))
            };

            _horizontalContainer.Set("custom_constants/separation", -200);
        }
    }
}
