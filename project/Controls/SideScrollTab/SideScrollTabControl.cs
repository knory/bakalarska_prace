using Controls;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace Controls
{
    public class SideScrollTabControl : SideScrollableControl<Label, string>
    {
        protected CenterContainer _centerContainer;
        public DynamicFont Font { get; set; }

        public event EventHandler<SelectedValueEventArgs> TabChanged;

        public override void _Ready()
        {
            _leftButton = GetNode<TextureButton>("LeftButton");
            _rightButton = GetNode<TextureButton>("RightButton");
            _centerContainer = GetNode<CenterContainer>("CenterContainer");
            _contentContainer = _centerContainer.GetNode<HBoxContainer>("ContentContainer");
        }

        protected override ICollection<Label> TransformPossibleValues(string[] possibleValues)
        {
            var tabLabels = new Label[possibleValues.Length];

            for (int i = 0; i < possibleValues.Length; i++)
            {
                var label = new Label();

                if (Font != null)
                {
                    label.AddFontOverride("font", Font);
                }

                label.Text = possibleValues[i];
                tabLabels[i] = label;
            }

            var maxLabelWidth = tabLabels.Max(l => l.GetMinimumSize().x);
            _centerContainer.RectMinSize = new Vector2(maxLabelWidth, Font.Size);;

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

        /// <summary>
        /// Handler of the scroll event.
        /// </summary>
        protected void OnTabChanged()
        {
            TabChanged?.Invoke(this, new SelectedValueEventArgs(){ SelectedValue = _leftMostIndex });
        }
    }
}