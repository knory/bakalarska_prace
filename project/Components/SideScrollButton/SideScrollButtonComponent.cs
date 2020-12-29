using Components;
using Controls;
using Godot;
using System;
using System.Collections.Generic;
using Utils;

namespace Components
{
    public abstract class SideScrollButtonComponent : Component<HashSet<int>>
    {
        protected SideScrollWithBackgroundControl _sideScrollWithBackgroundControl;
        protected Texture[] _backgroundTextures;
        protected Texture _leftButtonTexture;
        protected Texture _rightButtonTexture;
        protected Texture _addButtonTexture;
        protected Texture _removeButtonTexture;

        public void Init()
        {
            _sideScrollWithBackgroundControl.OnSelected += OnValueAdded;
            _sideScrollWithBackgroundControl.OnDeselected += OnValueRemoved;
            _sideScrollWithBackgroundControl.OnScrolled += OnScrolled;
            
            _sideScrollWithBackgroundControl.Init(_backgroundTextures, _leftButtonTexture, _rightButtonTexture,
                _addButtonTexture, _removeButtonTexture);
            
            SetValue(new HashSet<int>());
            DefaultValue = new HashSet<int>();
        }

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            GetCommonNodes();

            _sideScrollWithBackgroundControl = _windowWrapper.GetNode<SideScrollWithBackgroundControl>("SideScrollWithBackgroundControl");

            Init();
        }

        public void OnValueAdded(object sender, SelectedValueEventArgs eventArgs)
        {
            SelectedValue.Add(eventArgs.SelectedValue);
        }

        public void OnValueRemoved(object sender, SelectedValueEventArgs eventArgs)
        {
            SelectedValue.Remove(eventArgs.SelectedValue);
        }

        public virtual void OnScrolled(object sender, EventArgs eventArgs)
        { }

        public override void ResetState()
        {
            _sideScrollWithBackgroundControl.DeselectAll();
            _sideScrollWithBackgroundControl.ResetState();
            SetValue(new HashSet<int>());
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
            _sideScrollWithBackgroundControl.EnableControl();
        }

        public override void DisableComponent()
        {
            _sideScrollWithBackgroundControl.DisableControl();
        }
    }
}