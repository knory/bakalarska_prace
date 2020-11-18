using Godot;
using System;

public class ProgressBarComponent : Component
{
    private ProgressBarSideScrollControl _progressBarSideScrollControl;
    private ProgressBarState[] _progressBarStates;

    public void Init()
    {
        _progressBarSideScrollControl.Init(_progressBarStates, 1, false);
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _progressBarSideScrollControl = GetNode<ProgressBarSideScrollControl>("ProgressBarSideScrollControl");
        _progressBarSideScrollControl.ValueChanged += ChangeValue;

        var statesCount = Constants.ProgressBarResources.Length;
        _progressBarStates = new ProgressBarState[statesCount];
        
        for (int i = 0; i < statesCount; i++)
        {
            _progressBarStates[i] = new ProgressBarState{ Id = i + 1, Texture = (Texture)GD.Load(Constants.ProgressBarResources[i].TexturePath)};
        }

        Init();
    }

    public override bool CheckSelectedValue(object expectedValue = null)
    {
        return (int?)expectedValue == (int?)SelectedValue;
    }

    public void ChangeValue(object sender, SelectedValueEventArgs e)
    {
        SetValue(e.SelectedValue);
    }

    protected override void SetValue(object newValue)
    {
        SelectedValue = (int)newValue;
    }
}
