using Components;
using Controls;
using Godot;
using System;
using System.Collections.Generic;
using Utils;

namespace Components
{
    public class SingleSelectComponent : Component<int>
    {
        private HBoxContainer _horizontalContainer;
        private List<ClickableControl> _clickableComponents;

        public void Init() 
        {
            var clickableComponentScene = (PackedScene)ResourceLoader.Load("res://Controls/Clickable/ClickableControl.tscn");
            var text1 = (Texture)GD.Load($"{Constants.SpriteNames[0]}");
            var text2 = (Texture)GD.Load($"{Constants.SpriteNames[1]}");

            for (int i = 1; i <= 3; i++) {
                var clickableComponentInstance = (ClickableControl)clickableComponentScene.Instance();
                
                clickableComponentInstance.Init(text1, text2, i);
                clickableComponentInstance.Selected += OnValueSelected;
                clickableComponentInstance.Deselected += OnValueDeselected;

                _horizontalContainer.AddChild(clickableComponentInstance);
                _clickableComponents.Add(clickableComponentInstance);
            }

            DefaultValue = -1;
            SetValue(DefaultValue);
        }

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            _clickableComponents = new List<ClickableControl>();
            _horizontalContainer = GetNode<HBoxContainer>("HorizontalContainer");
            Init();
        }

        public void OnValueSelected(object sender, SelectedValueEventArgs e)
        {
            foreach (var item in _clickableComponents)
            {
                if (item.Value != e.SelectedValue)
                {
                    item.Deselect();
                }
                else
                {
                    item.Select();
                }
            }

            SetValue(e.SelectedValue);
        }

        public void OnValueDeselected(object sender, SelectedValueEventArgs e)
        {
            foreach (var item in _clickableComponents)
            {
                item.Deselect();
            }

            SetValue(DefaultValue);
        }

        public override void ResetState()
        {
            foreach (var item in _clickableComponents)
            {
                item.Deselect();
            }
            
            base.ResetState();
        }

        public override void ActivateComponent()
        {
            foreach (var item in _clickableComponents)
            {
                item.IsActive = true;
            }
        }

        public override void DeactivateComponent()
        {
            foreach (var item in _clickableComponents)
            {
                item.IsActive = false;
            }
        }
    }
}