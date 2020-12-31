using Components;
using Godot;
using Models;
using System;
using Utils;

namespace Scenes.Nongamified
{
    public class Time : DoubleDropdownComponent
    {
        private string _resourcesPath = $"{Constants.NongamifiedResourcesPath}Time/";

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            base._Ready();
            SetupView();
            Init();

            var popup1 = _optionButton1.GetPopup();
            for (int i = 0; i < Constants.TIME_OPTIONS_COUNT + 1; i++)
            {
                popup1.SetItemAsRadioCheckable(i, false);
            }

            var popup2 = _optionButton2.GetPopup();
            for (int i = 0; i < Constants.TIME_OPTIONS_COUNT + 1; i++)
            {
                popup2.SetItemAsRadioCheckable(i, false);
            }
        }

        protected override void SetupView()
        {
            var options1 = new DropdownModel[Constants.TIME_OPTIONS_COUNT + 1];
            var options2 = new DropdownModel[Constants.TIME_OPTIONS_COUNT + 1];

            for (int i = 0; i < Constants.TIME_OPTIONS_COUNT; i++)
            {
                options1[i + 1] = new DropdownModel { Id = i, Text = (i * 2).ToString() };
                options2[i + 1] = new DropdownModel { Id = i, Text = (i * 5).ToString("00") };
            }

            var defaultModel = new DropdownModel { Id = -1, Text = "--" };
            options1[0] = defaultModel;
            options2[0] = defaultModel;

            _listOfOptions1 = options1;
            _listOfOptions2 = options2;

            _optionButton1.RectMinSize = new Vector2(84, 49);
            _optionButton2.RectMinSize = new Vector2(84, 49);

            _optionButton1.RectPosition = new Vector2(40, 50);
            _optionButton2.RectPosition = new Vector2(165, 50);

            _background.Texture = (Texture)GD.Load($"{_resourcesPath}time_background.png");
            _background.Position = new Vector2(156, 0);

            var fontLight = (DynamicFont)GD.Load($"{_resourcesPath}montserrat_light.tres");
            fontLight.Size = 24;

            _title.AddFontOverride("font", fontLight);
            _title.Text = "Čas";

            var fontMedium = (DynamicFont)GD.Load($"{_resourcesPath}montserrat_medium.tres");
            fontMedium.Size = 24;

            _label.AddFontOverride("font", fontMedium);
            _label.Text = ":";
            _label.RectPosition = new Vector2(142, 58);

            _windowWrapper.RectPosition = new Vector2(15, -90);
        }
    }
}
