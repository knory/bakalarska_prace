using Components;
using Godot;
using System;
using Utils;

namespace NongamifiedScenes
{
    public class Rating : RatingComponent
    {
        private MarginContainer _commentsMarginContainer;
        private VBoxContainer _commentsContainer;

        private string _resourcesPath = $"{Constants.NongamifiedResourcesPath}Rating/";

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            base._Ready();
            Init(Constants.RATING_NUMBER_OF_ITEMS);
        }

        protected override void SetupView()
        {
            _textureOff = (Texture)GD.Load($"{_resourcesPath}star_off.png");
            _textureOn = (Texture)GD.Load($"{_resourcesPath}star_on.png");

            _background.Texture = (Texture)GD.Load($"{_resourcesPath}rating_background.png");
            _background.Position = new Vector2(181, 155);

            var font = (DynamicFont)GD.Load($"{_resourcesPath}montserrat_light.tres");
            font.Size = 24;
            _title.AddFontOverride("font", font);
            _title.Text = "Ohodnotit";

            _windowWrapper.RectPosition = new Vector2(25, 10);
            _windowWrapper.Set("custom_constants/separation", 21);

            _marginContainer.Set("custom_constants/margin_left", 28);

            _horizontalContainer.Set("custom_constants/separation", 15);

            _commentsMarginContainer = _windowWrapper.GetNode<MarginContainer>("CommentsMarginContainer");
            _commentsMarginContainer.Set("custom_constants/margin_left", 7);

            _commentsContainer = _commentsMarginContainer.GetNode<VBoxContainer>("CommentsContainer");
            _commentsContainer.Set("custom_constants/separation", 15);

            var lineTexture = (Texture)GD.Load($"{_resourcesPath}line.png");
            var comments = new string[]
            {
                "skvělé\nHana",
                "Líbí se mi tato verze.\nPetr"
            };

            foreach (var comment in comments)
            {
                var line = new TextureRect();
                line.Texture = lineTexture;

                _commentsContainer.AddChild(line);

                var margin = new MarginContainer();
                margin.Set("custom_constants/margin_left", 10);

                var label = new Label();
                label.AddFontOverride("font", font);
                label.Text = comment;

                margin.AddChild(label);
                _commentsContainer.AddChild(margin);
            }
        }
    }
}