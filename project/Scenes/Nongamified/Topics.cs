using Components;
using Godot;
using System;
using Utils;

namespace Scenes.Nongamified
{
    public class Topics : SingleSelectComponent
    {
        private string _resourcesPath = $"{Constants.NongamifiedResourcesPath}Topics/";

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
                ((Texture)GD.Load($"{_resourcesPath}finance_off.png"), (Texture)GD.Load($"{_resourcesPath}finance_on.png")),
                ((Texture)GD.Load($"{_resourcesPath}statistics_off.png"), (Texture)GD.Load($"{_resourcesPath}statistics_on.png")),
                ((Texture)GD.Load($"{_resourcesPath}health_off.png"), (Texture)GD.Load($"{_resourcesPath}health_on.png")),
                ((Texture)GD.Load($"{_resourcesPath}science_off.png"), (Texture)GD.Load($"{_resourcesPath}science_on.png")),
                ((Texture)GD.Load($"{_resourcesPath}sport_off.png"), (Texture)GD.Load($"{_resourcesPath}sport_on.png")),
                ((Texture)GD.Load($"{_resourcesPath}other_off.png"), (Texture)GD.Load($"{_resourcesPath}other_on.png")),
            };

            _horizontalContainer.Set("custom_constants/separation", 30);
        }
    }
}
