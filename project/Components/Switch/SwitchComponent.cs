using Components;
using Controls;
using Godot;
using System;
using Utils;

namespace Components
{
    public class SwitchComponent : Component<bool>
    {
        private ClickableControl _clickableControl;

        public void Init()
        {
            DefaultValue = false;
            SetValue(DefaultValue);
        }

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            var text1 = (Texture)GD.Load($"{Constants.SpriteNames[0]}");
            var text2 = (Texture)GD.Load($"{Constants.SpriteNames[1]}");

            _clickableControl = GetNode<ClickableControl>("ClickableControl");
            _clickableControl.Init(text1, text2, 0);
            _clickableControl.Selected += OnSelected;
            _clickableControl.Deselected += OnDeselected;
        }

        public override void ResetState()
        {
            _clickableControl.Deselect();
            base.ResetState();
        }

        public void OnSelected(object sender, EventArgs eventArgs)
        {
            SetValue(true);
        }

        public void OnDeselected(object sender, EventArgs eventArgs)
        {
            SetValue(false);
        }

        public override void EnableComponent()
        {
            _clickableControl.Disabled = false;
        }

        public override void DisableComponent()
        {
            _clickableControl.Disabled = true;
        }
    }
}