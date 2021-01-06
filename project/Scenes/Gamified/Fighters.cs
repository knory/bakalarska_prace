using Components;
using Godot;
using Models;
using System;
using Utils;

namespace Scenes.Gamified
{
    public class Fighters : TeammateComponent
    {
        private static readonly string _resourcesPath = $"{Constants.GamifiedResourcesPath}Fighters/";

        private readonly TeammateResource[] _teammateResources = new TeammateResource[8];

        public override void _Ready()
        {
            var teammates = new (string, string)[]
            {
                ("kobold", "kobold"),
                ("trollka", "troll"),
                ("skřítka", "dwarf"),
                ("půlorka", "half_orc"),
                ("ogryně", "ogre"),
                ("gnóm", "gnome"),
                ("zlobr", "giant"),
                ("džin", "gin")
            };

            for (int i = 0; i < teammates.Length; i++)
            {
                _teammateResources[i] = new TeammateResource
                {
                    Id = i,
                    Name = teammates[i].Item1.ToUpper(),
                    BigTexturePath = $"{_resourcesPath}{teammates[i].Item2}_big.png",
                    SmallTexturePath = $"{_resourcesPath}{teammates[i].Item2}_small.png"
                };
            }

            base._Ready();

            _teammateNameFont = (DynamicFont)GD.Load($"{_resourcesPath}bebas_neue.tres");
            _teammateNameFont.Size = 21;
            _teammateNameFont.OutlineColor = new Color("#ffffff");
            _teammateNameFont.OutlineSize = 4;

            _newTeammateLabelColor = new Color("#f2173c");
            _addedTeammateLabelColor = new Color("#1994ff");

            _teammateVerticalSeparation = 5;

            _addIcon = (Texture)GD.Load($"{_resourcesPath}plus_button.png");
            _removeIcon = (Texture)GD.Load($"{_resourcesPath}minus_button.png");

            var leftButton = (Texture)GD.Load($"{_resourcesPath}left_button.png");
            var rightButton = (Texture)GD.Load($"{_resourcesPath}right_button.png");

            Init(_teammateResources, leftButton, rightButton);
        }

        protected override void SetupView()
        {
            _teammatesSideScrollControl.Set("custom_constants/separation", 38);
            _verticalContainer.Set("custom_constants/separation", 233);
            _marginContainer.Set("custom_constants/margin_right", 35);
            _marginContainer.Set("custom_constants/margin_left", 35);
            _marginContainer.Set("custom_constants/margin_top", -80);

            _newTeammatesWrapper.Set("custom_constants/separation", 12);
            _addedTeammatesWrapper.Set("custom_constants/separation", 27);
            _teammatesHorizontalContainer.Set("custom_constants/separation", 13);
            _windowWrapper.Set("custom_constants/separation", 15);

            _background.Texture = (Texture)GD.Load($"{_resourcesPath}background.png");
            _background.Position = new Vector2(212, 261);

            _windowWrapper.RectPosition = new Vector2(18, 170);

            _sideScrollMarginContainer.Set("custom_constants/margin_left", 18);

            _teammatesSideScrollControl.GetNode<TextureButton>("LeftButton").RectPosition = new Vector2(-60, 55);
            _teammatesSideScrollControl.GetNode<TextureButton>("RightButton").RectPosition = new Vector2(370, 55);
        }
    }
}
