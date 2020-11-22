using Controls;
using Godot;
using System;
using System.Collections.Generic;
using Utils;

namespace Controls
{
    public class SideScrollTabControl : SideScrollableControl<Label, string>
    {
        public event EventHandler<SelectedValueEventArgs> TabChanged;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            
        }

        protected override ICollection<Label> TransformPossibleValues(string[] possibleValues)
        {
            var tabLabels = new Label[possibleValues.Length];

            for (int i = 0; i < possibleValues.Length; i++)
            {
                var label = new Label();
                label.Text = possibleValues[i];
                tabLabels[i] = label;
            }

            return tabLabels;
        }

        protected override void OnScrollLeft()
        {
            base.OnScrollLeft();

            OnTabChanged();
        }

        protected override void OnScrollRight()
        {
            base.OnScrollRight();

            OnTabChanged();
        }

        protected void OnTabChanged()
        {
            TabChanged?.Invoke(this, new SelectedValueEventArgs(){ SelectedValue = _leftMostIndex });
        }
    }
}