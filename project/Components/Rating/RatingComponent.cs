using Components;
using Controls;
using Godot;
using System;
using Utils;

namespace Components
{
    public class RatingComponent : Component<int>
    {
        private HBoxContainer _horizontalContainer;

        public void Init()
        {
            DefaultValue = -1;
            SetValue(DefaultValue);

            var clickableControlPackedScene = (PackedScene)ResourceLoader.Load("res://Controls/Clickable/ClickableControl.tscn");
            var text1 = (Texture)GD.Load($"{Constants.SpriteNames[0]}");
            var text2 = (Texture)GD.Load($"{Constants.SpriteNames[1]}");

            for (int i = 0; i < Constants.RATING_POSSIBLE_VALUES; i++)
            {
                var control = (ClickableControl)clickableControlPackedScene.Instance();
                control.Init(text1, text2, i);
                control.Selected += OnValueSelected;
                control.Deselected += OnValueDeselected;
                _horizontalContainer.AddChild(control);
            }
        }

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            _horizontalContainer = GetNode<HBoxContainer>("HorizontalContainer");
            Init();
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

        public void OnValueDeselected(object sender, SelectedValueEventArgs eventArgs)
        {
            SetValue(DefaultValue);

            foreach (ClickableControl item in _horizontalContainer.GetChildren())
            {
                item.Deselect();
            }
        }

        public override void ActivateComponent()
        {
            throw new NotImplementedException();
        }

        public override void DeactivateComponent()
        {
            throw new NotImplementedException();
        }
    }
}