using Components;
using Godot;
using Models;
using System;
using Utils;

namespace Scenes.Nongamified
{
    public class Teammates : TeammateComponent
    {
        private static readonly string _resourcesPath = $"{Constants.NongamifiedResourcesPath}Teammates/";
        private static readonly string _sharedResourcesPath = $"{Constants.NongamifiedResourcesPath}Shared/";

        private readonly TeammateResource[] _teammateResources = new TeammateResource[8];

        public override void _Ready()
        {
            var teammates = new (string, string)[]
            {
                ("Evžen", "evzen"),
                ("Jonáš", "jonas"),
                ("Katka", "katka"),
                ("Lukáš", "lukas"),
                ("Milena", "milena"),
                ("Pavel", "pavel"),
                ("Renata", "renata"),
                ("Tereza", "tereza")
            };

            for (int i = 0; i < teammates.Length; i++)
            {
                _teammateResources[i] = new TeammateResource
                {
                    Id = i,
                    Name = teammates[i].Item1,
                    BigTexturePath = $"{_resourcesPath}{teammates[i].Item2}_big.png",
                    SmallTexturePath = $"{_resourcesPath}{teammates[i].Item2}_small.png"
                };
            }

            base._Ready();

            _teammateNameFont = (DynamicFont)GD.Load($"{_resourcesPath}montserrat_bold.tres");
            _teammateNameFont.Size = 18;

            _teammateVerticalSeparation = 16;

            _addIcon = (Texture)GD.Load($"{_resourcesPath}plus_button.png");
            _removeIcon = (Texture)GD.Load($"{_resourcesPath}minus_button.png");

            var leftButton = (Texture)GD.Load($"{_sharedResourcesPath}left_arrow.png");
            var rightButton = (Texture)GD.Load($"{_sharedResourcesPath}right_arrow.png");

            Init(_teammateResources, leftButton, rightButton);
        }

        protected override void SetupView()
        {
            _teammatesSideScrollControl.Set("custom_constants/separation", 38);
            _verticalContainer.Set("custom_constants/separation", 233);
            _marginContainer.Set("custom_constants/margin_right", 60);
            _marginContainer.Set("custom_constants/margin_left", 60);

            _newTeammatesWrapper.Set("custom_constants/separation", 12);
            _addedTeammatesWrapper.Set("custom_constants/separation", 27);
            _teammatesHorizontalContainer.Set("custom_constants/separation", 33);
            _windowWrapper.Set("custom_constants/separation", 15);

            var font = (DynamicFont)GD.Load($"{_resourcesPath}montserrat_light.tres");
            font.Size = 24;

            var title = _windowWrapper.GetNode<Label>("Title");
            title.AddFontOverride("font", font);
            title.Text = "Tým";

            var sprite = GetNode<Sprite>("Background");
            sprite.Texture = (Texture)GD.Load($"{_resourcesPath}Box.png");
            sprite.Position = new Vector2(306, 253);

            _windowWrapper.RectSize = new Vector2(570, 44);
            _windowWrapper.RectPosition = new Vector2(18, 4);

            _newTeammatesLabel.AddFontOverride("font", font);
            _newTeammatesLabel.Text = "Přidat do týmu";
            _addedTeammatesLabel.AddFontOverride("font", font);
            _addedTeammatesLabel.Text = "Odebrat z týmu";

            _sideScrollMarginContainer.Set("custom_constants/margin_left", 18);

            _teammatesSideScrollControl.GetNode<TextureButton>("LeftButton").RectPosition = new Vector2(-20, 55);
            _teammatesSideScrollControl.GetNode<TextureButton>("RightButton").RectPosition = new Vector2(540, 55);
        }
    }
}
