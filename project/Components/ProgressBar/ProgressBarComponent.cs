using Components;
using Controls;
using Godot;
using Models;
using System;
using Utils;

namespace Components
{
    public abstract class ProgressBarComponent : Component<int>
    {
        protected MarginContainer _marginContainer;
        protected ProgressBarSideScrollControl _progressBarSideScrollControl;
        protected ProgressBarState[] _progressBarStates;
        protected Texture _leftButtonTexture;
        protected Texture _rightButtonTexture;

        public void Init()
        {
            _progressBarSideScrollControl.Init(_progressBarStates, 1, false, _leftButtonTexture, _rightButtonTexture);
        }

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            GetCommonNodes();

            _marginContainer = _windowWrapper.GetNode<MarginContainer>("MarginContainer");
            _progressBarSideScrollControl = _marginContainer.GetNode<ProgressBarSideScrollControl>("ProgressBarSideScrollControl");
            _progressBarSideScrollControl.ValueChanged += ChangeValue;

            DefaultValue = 0;
            SetValue(DefaultValue);
        }

        public void ChangeValue(object sender, SelectedValueEventArgs e)
        {
            SetValue(e.SelectedValue);
        }

        public override void ResetState()
        {
            _progressBarSideScrollControl.ResetState();
            base.ResetState();
        }

        public override void EnableComponent()
        {
            _progressBarSideScrollControl.EnableControl();
        }

        public override void DisableComponent()
        {
            _progressBarSideScrollControl.DisableControl();
        }
    }
}