using Controls;
using Godot;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace Controls
{
    public class ProgressBarSideScrollControl : SideScrollableControl<ProgressBarState, ProgressBarState>
    {
        public event EventHandler<SelectedValueEventArgs> ValueChanged;

        public override void _Ready()
        {
            base._Ready();
        }

        /// <summary>
        /// Initializes the control with provided values.
        /// </summary>
        /// <param name="possibleValues">Array of possible values</param>
        /// <param name="canJumpBounds">Sets whether the control can jump between first and last value</param>
        /// <param name="leftButton">Texture of the left button</param>
        /// <param name="rightButton">Texture of the right button</param>
        public void Init(ProgressBarState[] possibleValues, bool canJumpBounds, Texture leftButton, Texture rightButton)
        {
            base.Init(possibleValues, 1, canJumpBounds, leftButton, rightButton);

            _leftButton.Connect("pressed", this, nameof(OnValueChanged));
            _rightButton.Connect("pressed", this, nameof(OnValueChanged));
        }

        protected override ICollection<ProgressBarState> TransformPossibleValues(ProgressBarState[] possibleValues)
        {
            return possibleValues;
        }

        /// <summary>
        /// Value changed handler.
        /// </summary>
        public void OnValueChanged()
        {
            ValueChanged?.Invoke(this, new SelectedValueEventArgs { SelectedValue = ((ProgressBarState[])_possibleValues)[_leftMostIndex].Id });
        }
    }
}