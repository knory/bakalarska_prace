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

        public void Init(Texture leftButton, Texture rightButton)
        {
            _progressBarSideScrollControl.Init(_progressBarStates, 1, false, leftButton, rightButton);
        }

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            _progressBarSideScrollControl = GetNode<ProgressBarSideScrollControl>("ProgressBarSideScrollControl");
            _progressBarSideScrollControl.ValueChanged += ChangeValue;

            var statesCount = Constants.ProgressBarResources.Length;
            _progressBarStates = new ProgressBarState[statesCount];

            DefaultValue = 1;
            SetValue(DefaultValue);
            
            for (int i = 0; i < statesCount; i++)
            {
                _progressBarStates[i] = new ProgressBarState{ Id = i + 1, Texture = (Texture)GD.Load(Constants.ProgressBarResources[i].TexturePath)};
            }

            //TODO FIX
            Init(null, null);
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

        public override bool IsModified()
        {
            //default value is valid action as well
            return true;
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