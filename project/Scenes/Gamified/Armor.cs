using Components;
using Godot;
using System;
using Utils;

namespace Scenes.Gamified
{
    public class Armor : SideScrollSelectListComponent
    {
        private readonly string _resourcePath = $"{Constants.GamifiedResourcesPath}Armor/";

        private readonly string[] _armorNames = new string[12] { "Božská", "Falérová", "Kožená", "Kroužková", "Lamelová", "Magická", "Plátová", "Prošívaná", "Šupinová", "Titánská", "Vlněná", "Žádná" };
        private readonly string[] _contentHeadline = new string[1] { "Oleje" };

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            base._Ready();

            _gridMarginContainer = _tableContainer.GetNode<MarginContainer>("GridMarginContainer");

            var leftButton = (Texture)GD.Load($"{_resourcePath}left_button.png");
            var rightButton = (Texture)GD.Load($"{_resourcePath}right_button.png");
            Init(Constants.CALENDAR_VALUES_PER_LIST, _armorNames, 7, _contentHeadline, leftButton, rightButton, new Color("#eaff00"));

            foreach (var item in _gridHeadline.GetChildren())
            {
                if (!(item is Label label)) continue;

                label.RectMinSize = new Vector2(38, 38);
                label.SizeFlagsVertical = (int)SizeFlags.ExpandFill;
                label.SizeFlagsHorizontal = (int)SizeFlags.ExpandFill;
            }
        }

        protected override void SetupView()
        {
            _tabHeaderFont = (DynamicFont)GD.Load($"{_resourcePath}bebas_neue.tres");
            _tabHeaderFont.Size = 25;

            _gridHeaderFont = (DynamicFont)GD.Load($"{_resourcePath}chiller_grid_header.tres");
            _gridHeaderFont.Size = 25;

            _gridDataLabelFont = (DynamicFont)GD.Load($"{_resourcePath}chiller_grid_data.tres");
            _gridDataLabelFont.Size = 21;

            _gridContainer.Set("custom_constants/vseparation", -3);
            _gridContainer.Set("custom_constants/hseparation", -3);

            _headlineMarginContainer.Set("custom_constants/margin_left", 190);
            _gridMarginContainer.Set("custom_constants/margin_left", 95);
            _gridMarginContainer.Set("custom_constants/margin_top", -10);

            _gridHeadline.Set("custom_constants/separation", 30);
            _gridHeadline.SizeFlagsHorizontal = (int)SizeFlags.ExpandFill;
            _gridHeadline.SizeFlagsVertical = (int)SizeFlags.ShrinkCenter;
            _gridHeadline.RectPosition = new Vector2(8, 0);

            _windowWrapper.RectPosition = new Vector2(20, 170);

            _verticalContainer.Set("custom_constants/separation", 15);

            _background.Texture = (Texture)GD.Load($"{_resourcePath}background.png");
            _background.Position = new Vector2(235, 217);

            _sideScrollTabControl.GetNode<TextureButton>("LeftButton").RectPosition = new Vector2(20, -25);
            _sideScrollTabControl.GetNode<TextureButton>("RightButton").RectPosition = new Vector2(350, -25);

            var sideScrollCenterContainer = _sideScrollTabControl.GetNode<CenterContainer>("CenterContainer");
            sideScrollCenterContainer.RectPosition = new Vector2(168, 0);

            _deselectedTexture = (Texture)GD.Load($"{_resourcePath}unselected_item.png");
            _selectedTexture = (Texture)GD.Load($"{_resourcePath}selected_item.png");
        }
    }
}
