using Godot;
using System;

namespace Models
{
    public class SelectListModel
    {
        public int NumberOfItems { get; set; }
        public int GridOffset { get; set; }
        public int SelectedItem { get; set; } = -1;
    }
}