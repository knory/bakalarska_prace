using Components;
using Controls;
using Godot;
using System;
using Utils;

namespace Components
{
    public abstract class RatingComponent : Component<int>
    {
        protected MarginContainer _marginContainer;
        protected HBoxContainer _horizontalContainer;
        protected Texture _textureOff;
        protected Texture _textureOn;

        /// <summary>
        /// Initializes component's value and possible values.
        /// </summary>
        public void Init(int numberOfItems)
        {
            DefaultValue = -1;
            SetValue(DefaultValue);

            var clickableControlPackedScene = (PackedScene)GD.Load("res://Controls/Clickable/ClickableControl.tscn");

            for (int i = 0; i < numberOfItems; i++)
            {
                var control = (ClickableControl)clickableControlPackedScene.Instance();
                control.Init(_textureOff, _textureOn, i);
                control.Selected += OnValueSelected;
                control.Deselected += OnValueDeselected;
                _horizontalContainer.AddChild(control);
            }
        }

        public override void _Ready()
        {
            GetCommonNodes();
            _marginContainer = _windowWrapper.GetNode<MarginContainer>("MarginContainer");
            _horizontalContainer = _marginContainer.GetNode<HBoxContainer>("HorizontalContainer");
            SetupView();
        }

        public override void ResetState()
        {
            foreach (var item in _horizontalContainer.GetChildren())
            {
                if (item is ClickableControl)
                {
                    ((ClickableControl)item).Deselect();
                }
            }

            base.ResetState();
        }

        /// <summary>
        /// Changes the selected value to the specified value and deselects all other control nodes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        public void OnValueSelected(object sender, SelectedValueEventArgs eventArgs)
        {
            SetValue(eventArgs.SelectedValue);

            foreach (ClickableControl item in _horizontalContainer.GetChildren())
            {
                item.Deselect();

                if (item.Value <= eventArgs.SelectedValue)
                {
                    item.SetSelectedTexture();

                    if (item.Value == eventArgs.SelectedValue)
                    {
                        item.Select();
                    }
                }
            }
        }

        /// <summary>
        /// Sets the selected value to default and deselects all control nodes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        public void OnValueDeselected(object sender, SelectedValueEventArgs eventArgs)
        {
            SetValue(DefaultValue);

            foreach (var item in _horizontalContainer.GetChildren())
            {
                if (!(item is ClickableControl control)) continue;
                control.Deselect();
            }
        }

        public override void EnableComponent()
        {
            foreach (var item in _horizontalContainer.GetChildren())
            {
                if (!(item is ClickableControl control)) continue;
                control.Disabled = false;
            }
        }

        public override void DisableComponent()
        {
            foreach (var item in _horizontalContainer.GetChildren())
            {
                if (!(item is ClickableControl control)) continue;
                control.Disabled = true;
            }
        }
    }
}