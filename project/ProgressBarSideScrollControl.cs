using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class ProgressBarSideScrollControl : SideScrollableControl<ProgressBarState, ProgressBarState>
{
    public event EventHandler<SelectedValueEventArgs> ValueChanged;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

    public override void Init(ProgressBarState[] possibleValues, int valuesShown, bool canJumpBounds)
    {
        base.Init(possibleValues, valuesShown, canJumpBounds);

        _leftButton.Connect("pressed", this, nameof(OnValueChanged));
        _rightButton.Connect("pressed", this, nameof(OnValueChanged));
    }

    protected override ICollection<ProgressBarState> TransformPossibleValues(ProgressBarState[] possibleValues)
    {
        return possibleValues;
    }

    public void OnValueChanged()
    {
        ValueChanged?.Invoke(this, new SelectedValueEventArgs { SelectedValue = ((ProgressBarState[])_possibleValues)[_leftMostIndex].Id });
    }
}