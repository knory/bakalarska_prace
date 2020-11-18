using Godot;
using System;

public class SelectListModel
{
    public int NumberOfItems { get; set; }
    public int GridOffset { get; set; }
    public int SelectedItem { get; set; } = -1;
}