using Controls;
using Godot;
using System;

namespace Utils
{
    public class SelectedValueEventArgs : EventArgs
    {
        public int SelectedValue { get; set; }
    }

    public class TeammateControlClickedEventArgs : EventArgs
    {
        public TeammateControl SelectedValue { get; set; }
    }
}