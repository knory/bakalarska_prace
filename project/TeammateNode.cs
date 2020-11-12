using Godot;
using System;

public class TeammateNode : Control
{
    private VBoxContainer _verticalContainer;
    private TextureButton _teammateButton;
    private TextureRect _actionIcon;
    private Label _teammateName;

    public Teammate Teammate { get; private set; }

    public event EventHandler<TeammateNodeClickedEventArgs> Clicked;

    public void Init(Teammate teammate)
    {
        Teammate = teammate;

        _verticalContainer = GetNode<VBoxContainer>("VerticalContainer");
        _teammateButton = _verticalContainer.GetNode<TextureButton>("TeammateButton");
        _actionIcon = _teammateButton.GetNode<TextureRect>("ActionIcon");
        _teammateName = _verticalContainer.GetNode<Label>("TeammateName");

        _teammateButton.TextureNormal = ScaleTexture(teammate.Texture, 100, 100);
        
        var teammateButtonSize = _teammateButton.TextureNormal.GetSize();
        this.RectMinSize = _teammateButton.TextureNormal.GetSize();

        _teammateName.Text = teammate.Name;
        _actionIcon.Texture = teammate.IsAddedToTeam ? Constants.TeammateActionIcons["minus"] : Constants.TeammateActionIcons["plus"];

        var actionIconSize = _actionIcon.Texture.GetSize();
        _actionIcon.SetPosition(new Vector2(teammateButtonSize.x - actionIconSize.x, teammateButtonSize.y - actionIconSize.y));

        _teammateButton.Connect("pressed", this, "OnClick");
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    private void OnClick()
    {
        Clicked?.Invoke(this, new TeammateNodeClickedEventArgs(){ SelectedValue = this });
    }

    private Texture ScaleTexture(Texture texture, int width, int height)
    {
        var image = texture.GetData();
        if (width > 0 && height > 0)
        {
            image.Resize(width, height);
        }
        var itex = new ImageTexture();
        itex.CreateFromImage(image);
        return itex;
    }
}

public class TeammateNodeClickedEventArgs : EventArgs
{
    public TeammateNode SelectedValue { get; set; }
}