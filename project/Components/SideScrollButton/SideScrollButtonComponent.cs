using Components;
using Controls;
using Godot;
using System;
using System.Collections.Generic;
using Utils;

namespace Components
{
    public class SideScrollButtonComponent : Component
    {
        private SideScrollWithBackgroundControl _sideScrollWithBackgroundControl;

        public void Init()
        {
            _sideScrollWithBackgroundControl.Init(Constants.LABEL_WITH_BUTTON_RESOURCES, OnValueAdded, OnValueRemoved);
            SetValue(new HashSet<int>());
        }

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            _sideScrollWithBackgroundControl = GetNode<SideScrollWithBackgroundControl>("SideScrollWithBackgroundControl");

            Init();
        }

        public override bool CheckSelectedValue(object expectedValue = null)
        {
            var typedExpectedValue = (HashSet<int>)expectedValue;
            var selectedValue = (HashSet<int>)SelectedValue;

            return typedExpectedValue.SetEquals(selectedValue);
        }

        protected override void SetValue(object newValue)
        {
            SelectedValue = newValue;
        }

        public void OnValueAdded(object sender, SelectedValueEventArgs eventArgs)
        {
            var values = (HashSet<int>)SelectedValue ?? new HashSet<int>();

            values.Add(eventArgs.SelectedValue);

            SetValue(values);
        }

        public void OnValueRemoved(object sender, SelectedValueEventArgs eventArgs)
        {
            var values = (HashSet<int>)SelectedValue ?? new HashSet<int>();

            values.Remove(eventArgs.SelectedValue);

            SetValue(values);
        }
    }
}