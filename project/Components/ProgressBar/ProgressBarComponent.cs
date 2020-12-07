using Components;
using Controls;
using Godot;
using Models;
using System;
using Utils;

namespace Components
{
    public class ProgressBarComponent : Component<int>
    {
        private ProgressBarSideScrollControl _progressBarSideScrollControl;
        private ProgressBarState[] _progressBarStates;

        public void Init()
        {
            _progressBarSideScrollControl.Init(_progressBarStates, 1, false);
        }

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            _progressBarSideScrollControl = GetNode<ProgressBarSideScrollControl>("ProgressBarSideScrollControl");
            _progressBarSideScrollControl.ValueChanged += ChangeValue;

            var statesCount = Constants.ProgressBarResources.Length;
            _progressBarStates = new ProgressBarState[statesCount];

            DefaultValue = -1;
            SetValue(DefaultValue);
            
            for (int i = 0; i < statesCount; i++)
            {
                _progressBarStates[i] = new ProgressBarState{ Id = i + 1, Texture = (Texture)GD.Load(Constants.ProgressBarResources[i].TexturePath)};
            }

            Init();
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
    }
}