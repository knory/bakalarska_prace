using Components;
using Controls;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace Components
{
    public abstract class SingleSelectComponent : Component<int>
    {
        private List<ClickableControl> _clickableComponents;
        protected MarginContainer _marginContainer;
        protected HBoxContainer _horizontalContainer;
        protected (Texture, Texture)[] _textures;

        /// <summary>
        /// Initializes component's value and possible values.
        /// </summary>
        public void Init() 
        {
            var clickableComponentScene = (PackedScene)GD.Load("res://Controls/Clickable/ClickableControl.tscn");

            for (int i = 0; i < _textures.Length; i++) 
            {
                var clickableComponentInstance = (ClickableControl)clickableComponentScene.Instance();

                var texturePair = _textures[i];

                clickableComponentInstance.Init(texturePair.Item1, texturePair.Item2, i);
                clickableComponentInstance.Selected += OnValueSelected;
                clickableComponentInstance.Deselected += OnValueDeselected;

                _horizontalContainer.AddChild(clickableComponentInstance);
                _clickableComponents.Add(clickableComponentInstance);
            }

            DefaultValue = -1;
            SetValue(DefaultValue);
        }

        public override void _Ready()
        {
            GetCommonNodes();

            _clickableComponents = new List<ClickableControl>();
            _marginContainer = _windowWrapper.GetNode<MarginContainer>("MarginContainer");
            _horizontalContainer = _marginContainer.GetNode<HBoxContainer>("HorizontalContainer");
        }

        /// <summary>
        /// Sets the specified value as the selected value and deselects all other control nodes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Deselects all control nodes and resets the selected value to default.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        public override void EnableComponent()
        {
            foreach (var item in _clickableComponents)
            {
                item.Disabled = false;
            }
        }

        public override void DisableComponent()
        {
            foreach (var item in _clickableComponents)
            {
                item.Disabled = true;
            }
        }
    }
}