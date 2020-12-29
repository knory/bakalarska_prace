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
    public abstract class MultipleSelectComponent : Component<HashSet<int>>
    {
        protected MarginContainer _marginContainer;
        protected HBoxContainer _horizontalContainer;
        protected (Texture, Texture)[] _textures;
        private List<ClickableControl> _clickableControls;

        public void Init() 
        {
            var clickableComponentScene = (PackedScene)GD.Load("res://Controls/Clickable/ClickableControl.tscn");

            SetValue(new HashSet<int>());
            DefaultValue = new HashSet<int>();

            for (int i = 0; i < _textures.Length; i++)
            {
                var clickableComponentInstance = (ClickableControl)clickableComponentScene.Instance();

                var texturePair = _textures[i];

                clickableComponentInstance.Init(texturePair.Item1, texturePair.Item2, i);
                clickableComponentInstance.Selected += AddSelectedValue;
                clickableComponentInstance.Deselected += RemoveSelectedValue;
                
                _horizontalContainer.AddChild(clickableComponentInstance);
                _clickableControls.Add(clickableComponentInstance);
            }
        }

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            GetCommonNodes();

            _clickableControls = new List<ClickableControl>();
            _marginContainer = _windowWrapper.GetNode<MarginContainer>("MarginContainer");
            _horizontalContainer = _marginContainer.GetNode<HBoxContainer>("HorizontalContainer");
        }

        public override void ResetState()
        {
            foreach (var item in _clickableControls)
            {
                item.Deselect();
            }

            SetValue(new HashSet<int>());
        }

        public List<int> GetSelectedValues()
        {
            var result = new List<int>();
            foreach (var item in _clickableControls)
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

        protected override bool CheckSelectedValue(HashSet<int> expectedValue)
        {
            return expectedValue.SetEquals(SelectedValue);
        }

        public override bool IsModified()
        {
            return !DefaultValue.SetEquals(SelectedValue);
        }

        public override void EnableComponent()
        {
            foreach (var item in _clickableControls)
            {
                item.Disabled = false;
            }
        }

        public override void DisableComponent()
        {
            foreach (var item in _clickableControls)
            {
                item.Disabled = true;
            }
        }
    }
}
