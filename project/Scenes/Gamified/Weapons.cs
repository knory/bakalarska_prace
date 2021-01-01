using Components;
using Godot;
using System;
using Utils;

namespace Scenes.Gamified
{
    public class Weapons : SingleSelectComponent
    {
        private string _resourcesPath = $"{Constants.GamifiedResourcesPath}Weapons/";

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
                ((Texture)GD.Load($"{_resourcesPath}sword_off.png"), (Texture)GD.Load($"{_resourcesPath}sword_on.png")),
                ((Texture)GD.Load($"{_resourcesPath}slingshot_off.png"), (Texture)GD.Load($"{_resourcesPath}slingshot_on.png")),
                ((Texture)GD.Load($"{_resourcesPath}axe_off.png"), (Texture)GD.Load($"{_resourcesPath}axe_on.png")),
                ((Texture)GD.Load($"{_resourcesPath}bow_off.png"), (Texture)GD.Load($"{_resourcesPath}bow_on.png")),
                ((Texture)GD.Load($"{_resourcesPath}mace_off.png"), (Texture)GD.Load($"{_resourcesPath}mace_on.png")),
                ((Texture)GD.Load($"{_resourcesPath}nunchak_off.png"), (Texture)GD.Load($"{_resourcesPath}nunchak_on.png")),
            };

            _horizontalContainer.Set("custom_constants/separation", 37);

            _background.Texture = (Texture)GD.Load($"{_resourcesPath}background.png");
            _background.Position = new Vector2(480, 100);

            _marginContainer.Set("custom_constants/margin_left", 192);
            _marginContainer.Set("custom_constants/margin_top", 37);
        }
    }
}
