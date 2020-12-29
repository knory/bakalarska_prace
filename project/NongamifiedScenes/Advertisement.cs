using Components;
using Controls;
using Godot;
using System;
using Utils;

namespace NongamifiedScenes
{
    public class Advertisement : SideScrollButtonComponent
    {
        private string _resourcePath = $"{Constants.NongamifiedResourcesPath}Advertisement/";

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            _backgroundTextures = new Texture[]
            {
                (Texture)GD.Load($"{_resourcePath}free_coffee.png"),
                (Texture)GD.Load($"{_resourcePath}wellness.png"),
                (Texture)GD.Load($"{_resourcePath}balloon_flight.png")
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

            leftButton.RectPosition = new Vector2(14, 108);
            rightButton.RectPosition = new Vector2(556, 108);

            _windowWrapper.RectPosition = new Vector2(0, -10);

            AdjustControlPositioning();
        }

        public override void OnScrolled(object sender, EventArgs eventArgs)
        {
            AdjustControlPositioning();
        }

        private void AdjustControlPositioning()
        {
            var container = _sideScrollWithBackgroundControl.GetNode<HBoxContainer>("ContentContainer");
            var adControl = (ButtonWithBackgroundControl)container.GetChildren()[0];
            
            var background = adControl.GetNode<Sprite>("Background");
            var plusButton = adControl.GetNode<ClickableControl>("ClickableControl");

            background.Position = new Vector2(280, 110);
            plusButton.RectPosition = new Vector2(250, 160);
        }
    }
}
