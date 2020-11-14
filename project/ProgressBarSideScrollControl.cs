using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class ProgressBarSideScrollControl : SideScrollableControl<ProgressBarState, ProgressBarState>
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

    protected override ICollection<ProgressBarState> TransformPossibleValues(ProgressBarState[] possibleValues)
    {
        return possibleValues;
    }

    public int GetSelectedValue()
    {
        return ((ProgressBarState[])_possibleValues)[_leftMostIndex].Id;
    }
}
