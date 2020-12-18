using Components;
using Godot;
using Scenes;
using System;
using Utils;

public class NongamifiedGame : Game
{
    private Sprite _topBar;
    private TeammateComponent _teammateComponent;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _topBar = GetNode<Sprite>("TopBar");
        _teammateComponent = GetNode<TeammateComponent>("TeammateComponent");
        var leftButton = Resources.Nongamified.SideButtonTextures["left"];
        var rightButton = Resources.Nongamified.SideButtonTextures["right"];

        _teammateComponent.Init(Resources.Nongamified.TeammateResources, leftButton, rightButton);

        //TODO
        _topBar.Texture = (Texture)GD.Load("res://Resources/Nongamified/nongamified_top_bar.png");
        _topBar.Position = new Vector2(960, 46);
    }

    protected override int CountGainedScore()
    {
        return 0;
    }

    protected override void DisableComponents()
    {
        _teammateComponent.DisableComponent();
    }

    protected override void EnableComponents()
    {
        _teammateComponent.EnableComponent();
    }

    protected override void HideComponents()
    {
        _teammateComponent.Hide();

    }

    protected override void ShowComponents()
    {
        _teammateComponent.Show();
    }

    protected override void ResetComponents()
    {
        _teammateComponent.ResetState();
    }
}
