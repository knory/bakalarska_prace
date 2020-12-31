using Components;
using Godot;
using System;
using Utils;

namespace Scenes.Nongamified
{
    public class Calendar : SideScrollSelectListComponent
    {
        private readonly string _resourcePath = $"{Constants.NongamifiedResourcesPath}Calendar/";
        private readonly string _sharedResourcePath = $"{Constants.NongamifiedResourcesPath}Shared/";
        
        private readonly string[] _monthNames = new string[12] { "Leden", "Únor", "Březen", "Duben", "Květen", "Červen", "Červenec", "Srpen", "Září", "Říjen", "Listopad", "Prosinec" };
        private readonly string[] _dayNames = new string[7] { "Po", "Út", "St", "Čt", "Pá", "So", "Ne" };

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            base._Ready();

            var leftButton = (Texture)GD.Load($"{_sharedResourcePath}left_arrow.png");
            var rightButton = (Texture)GD.Load($"{_sharedResourcePath}right_arrow.png");
            Init(Constants.CALENDAR_VALUES_PER_LIST, _monthNames, _dayNames.Length, _dayNames, leftButton, rightButton);

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
            _tabHeaderFont = (DynamicFont)GD.Load($"{_resourcePath}montserrat_light.tres");
            _tabHeaderFont.Size = 24;

            _gridHeaderFont = (DynamicFont)GD.Load($"{_resourcePath}montserrat_bold.tres");
            _gridHeaderFont.Size = 18;

            _gridDataLabelFont = (DynamicFont)GD.Load($"{_resourcePath}montserrat_medium.tres");
            _gridDataLabelFont.Size = 18;

            _gridContainer.Set("custom_constants/vseparation", 5);
            _gridContainer.Set("custom_constants/hseparation", 30);
            _gridContainer.RectPosition = new Vector2(-8, 0);

            _gridHeadline.Set("custom_constants/separation", 30);
            _gridHeadline.SizeFlagsHorizontal = (int)SizeFlags.ExpandFill;
            _gridHeadline.SizeFlagsVertical = (int)SizeFlags.ShrinkCenter;
            _gridHeadline.RectPosition = new Vector2(8, 0);

            _marginContainer.Set("custom_constants/margin_left", 8);

            _verticalContainer.Set("custom_constants/separation", 40);

            var background = (Texture)GD.Load($"{_resourcePath}calendar_background.png");
            _background.Texture = background;
            _background.Position = new Vector2(235, 217);

            _title.Text = "Datum";
            _title.AddFontOverride("font", _tabHeaderFont);
            _title.RectPosition = new Vector2(18, 0);

            _windowWrapper.Set("custom_constants/separation", 30);
            _windowWrapper.RectPosition = new Vector2(10, 10);

            _sideScrollTabControl.GetNode<TextureButton>("LeftButton").RectPosition = new Vector2(10, 0);
            _sideScrollTabControl.GetNode<TextureButton>("RightButton").RectPosition = new Vector2(420, 0);

            var sideScrollCenterContainer = _sideScrollTabControl.GetNode<CenterContainer>("CenterContainer");
            sideScrollCenterContainer.RectPosition = new Vector2(168, 0);

            _deselectedTexture = (Texture)GD.Load($"{_resourcePath}unselected_day.png");
            _selectedTexture = (Texture)GD.Load($"{_resourcePath}selected_day.png");
        }
    }
}
