using Components;
using Controls;
using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace Components
{
    public class MultipleSelectComponent : Component<HashSet<int>>
    {
        private HBoxContainer _horizontalContainer;
        private List<ClickableControl> _clickableComponents;

        public void Init() 
        {
            var clickableComponentScene = (PackedScene)ResourceLoader.Load("res://Controls/Clickable/ClickableControl.tscn");
            var text1 = (Texture)GD.Load($"{Constants.SpriteNames[0]}");
            var text2 = (Texture)GD.Load($"{Constants.SpriteNames[1]}");
            var defaultVal = new HashSet<int>();
            SetValue(defaultVal);
            DefaultValue = defaultVal;

            for (int i = 1; i <= Constants.MULTIPLE_SELECT_VALUES_COUNT; i++) {
                var clickableComponentInstance = (ClickableControl)clickableComponentScene.Instance();
                
                clickableComponentInstance.Init(text1, text2, i);
                clickableComponentInstance.Selected += AddSelectedValue;
                clickableComponentInstance.Deselected += RemoveSelectedValue;
                
                _horizontalContainer.AddChild(clickableComponentInstance);
                _clickableComponents.Add(clickableComponentInstance);
            }
        }

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            _clickableComponents = new List<ClickableControl>();
            _horizontalContainer = GetNode<HBoxContainer>("HorizontalContainer");
            Init();
        }

        public override void ResetState()
        {
            foreach (var item in _clickableComponents)
            {
                item.Deselect();
            }
        }

        public void ActivateNodes()
        {
            foreach (var item in _clickableComponents)
            {
                item.IsActive = true;
            }
        }

        public void DeactivateNodes()
        {
            foreach (var item in _clickableComponents)
            {
                item.IsActive = false;
            }
        }

        public List<int> GetSelectedValues()
        {
            var result = new List<int>();
            foreach (var item in _clickableComponents)
            {
                if (item.IsSelected)
                    result.Add(item.Value);
            }

            return result;
        }

        public void AddSelectedValue(object sender, SelectedValueEventArgs eventArgs)
        {
            SelectedValue.Add(eventArgs.SelectedValue);
        }

        public void RemoveSelectedValue(object sender, SelectedValueEventArgs eventArgs)
        {
            SelectedValue.Remove(eventArgs.SelectedValue);
        }

        public override bool CheckSelectedValue(HashSet<int> expectedValue)
        {
            return expectedValue.SetEquals(SelectedValue);
        }

        public override bool IsModified()
        {
            return !DefaultValue.SetEquals(SelectedValue);
        }
    }
}
