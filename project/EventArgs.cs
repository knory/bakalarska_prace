using Godot;
using System;

public class SelectedValueEventArgs : EventArgs
{
    public int SelectedValue { get; set; }
}

public class TeammateNodeClickedEventArgs : EventArgs
{
    public TeammateNode SelectedValue { get; set; }
}