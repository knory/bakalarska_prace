using Godot;
using Models;
using System;
using Utils;

namespace Controls
{
    public class TeammateControl : Control
    {
        private VBoxContainer _verticalContainer;
        private TextureButton _teammateButton;
        private TextureRect _actionIcon;
        private Label _teammateName;

        public int VerticalContainerSeparation { get; set; }
        public DynamicFont Font { get; set; }

        public Teammate Teammate { get; private set; }

        public event EventHandler<TeammateControlClickedEventArgs> Clicked;

        public void Init(Teammate teammate, Texture addIcon, Texture removeIcon)
        {
            Teammate = teammate;
            var actionIcon = teammate.IsAddedToTeam ? removeIcon : addIcon;
            var usedTexture = teammate.IsAddedToTeam ? teammate.SmallTexture : teammate.BigTexture;

            _verticalContainer = GetNode<VBoxContainer>("VerticalContainer");
            _teammateButton = _verticalContainer.GetNode<TextureButton>("TeammateButton");
            _actionIcon = _teammateButton.GetNode<TextureRect>("ActionIcon");
            _teammateName = _verticalContainer.GetNode<Label>("CenterContainer/TeammateName");

            _teammateButton.TextureNormal = usedTexture;
            
            var teammateButtonSize = _teammateButton.TextureNormal.GetSize();
            this.RectMinSize = _teammateButton.TextureNormal.GetSize();

            _teammateName.Text = teammate.Name;
            _actionIcon.Texture = teammate.IsAddedToTeam ? actionIcon : actionIcon;

            var actionIconSize = _actionIcon.Texture.GetSize();
            _actionIcon.SetPosition(new Vector2(teammateButtonSize.x - actionIconSize.x, teammateButtonSize.y - actionIconSize.y));

            _teammateButton.Connect("pressed", this, nameof(OnClick));

            _verticalContainer.Set("custom_constants/separation", VerticalContainerSeparation);
            _teammateName.AddFontOverride("font", Font);
        }

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            
        }

        public void EnableControl()
        {
            _teammateButton.Disabled = false;
        }

        public void DisableControl()
        {
            _teammateButton.Disabled = true;
        }

        private void OnClick()
        {
            Clicked?.Invoke(this, new TeammateControlClickedEventArgs(){ SelectedValue = this });
        }
    }
}