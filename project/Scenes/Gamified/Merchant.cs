using Components;
using Godot;
using Models;
using System;
using Utils;

namespace Scenes.Gamified
{
    public class Merchant : DoubleDropdownComponent
    {
        private string _resourcesPath = $"{Constants.GamifiedResourcesPath}Merchant/";

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
                options1[i + 1] = new DropdownModel { Id = i, Text = (i + 1).ToString() };
                options2[i + 1] = new DropdownModel { Id = i, Text = (i + 1).ToString() };
            }

            var defaultModel = new DropdownModel { Id = -1, Text = "--" };
            options1[0] = defaultModel;
            options2[0] = defaultModel;

            _listOfOptions1 = options1;
            _listOfOptions2 = options2;

            _optionButton1.RectMinSize = new Vector2(84, 49);
            _optionButton2.RectMinSize = new Vector2(84, 49);

            _optionButton1.RectPosition = new Vector2(0, 150);
            _optionButton2.RectPosition = new Vector2(235, 100);

            _background.Texture = (Texture)GD.Load($"{_resourcesPath}background.png");
            _background.Position = new Vector2(150, 203);
        }
    }
}
