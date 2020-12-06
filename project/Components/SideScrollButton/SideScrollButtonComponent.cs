using Components;
using Controls;
using Godot;
using System;
using System.Collections.Generic;
using Utils;

namespace Components
{
    public class SideScrollButtonComponent : Component<HashSet<int>>
    {
        private SideScrollWithBackgroundControl _sideScrollWithBackgroundControl;

        public void Init()
        {
            _sideScrollWithBackgroundControl.Init(Constants.LABEL_WITH_BUTTON_RESOURCES, OnValueAdded, OnValueRemoved);
            
            var defaultVal = new HashSet<int>();
            SetValue(defaultVal);
            DefaultValue = defaultVal;
        }

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            _sideScrollWithBackgroundControl = GetNode<SideScrollWithBackgroundControl>("SideScrollWithBackgroundControl");

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