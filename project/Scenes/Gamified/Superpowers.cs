using Components;
using Controls;
using Godot;
using System;
using Utils;

namespace Scenes.Gamified
{
    public class Superpowers : SideScrollButtonComponent
    {
        private string _resourcePath = $"{Constants.GamifiedResourcesPath}Superpowers/";

        public override void _Ready()
        {
            _backgroundTextures = new Texture[]
            {
                (Texture)GD.Load($"{_resourcePath}fire.png"),
                (Texture)GD.Load($"{_resourcePath}water.png"),
                (Texture)GD.Load($"{_resourcePath}earth.png")
            };

            _addButtonTexture = (Texture)GD.Load($"{_resourcePath}add_button.png");
            _removeButtonTexture = (Texture)GD.Load($"{_resourcePath}remove_button.png");
            _leftButtonTexture = (Texture)GD.Load($"{_resourcePath}left_button.png");
            _rightButtonTexture = (Texture)GD.Load($"{_resourcePath}right_button.png");

            base._Ready();
            SetupView();
        }

        protected override void SetupView()
        {
            var leftButton = _sideScrollWithBackgroundControl.GetNode<TextureButton>("LeftButton");
            var rightButton = _sideScrollWithBackgroundControl.GetNode<TextureButton>("RightButton");

            leftButton.RectPosition = new Vector2(14, 75);
            rightButton.RectPosition = new Vector2(300, 75);

            _windowWrapper.RectPosition = new Vector2(0, -9);
            _windowWrapper.Set("custom_constants/separation", -35);

            _background.Texture = (Texture)GD.Load($"{_resourcePath}background.png");
            _background.Position = new Vector2(180, 145);

            var font = (DynamicFont)GD.Load($"{_resourcePath}chiller.tres");
            font.Size = 32;

            _title.RectMinSize = new Vector2(360, 0);
            _title.Align = Label.AlignEnum.Center;
            _title.AddFontOverride("font", font);
            _title.Text = "Super Schopnosti";

            AdjustControlPositioning();
        }

        public override void OnScrolled(object sender, EventArgs eventArgs)
        {
            AdjustControlPositioning();
        }

        /// <summary>
        /// Adjusts positioning of the control's nodes.
        /// </summary>
        private void AdjustControlPositioning()
        {
            var container = _sideScrollWithBackgroundControl.GetNode<HBoxContainer>("ContentContainer");
            var adControl = (ButtonWithBackgroundControl)container.GetChildren()[0];

            var background = adControl.GetNode<Sprite>("Background");
            var plusButton = adControl.GetNode<ClickableControl>("ClickableControl");

            background.Position = new Vector2(162, 105);
            plusButton.RectPosition = new Vector2(132, 160);
        }
    }
}
