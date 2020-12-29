using Controls;
using Godot;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace Controls
{
    public class SideScrollWithBackgroundControl : SideScrollableControl<ButtonWithBackgroundControl, Texture>
    {
        private Texture _addButtonTexture;
        private Texture _removeButtonTexture;
        public EventHandler<SelectedValueEventArgs> OnSelected;
        public EventHandler<SelectedValueEventArgs> OnDeselected;
        public EventHandler<EventArgs> OnScrolled;

        public void Init(Texture[] possibleValues, Texture leftButtonTexture, Texture rightButtonTexture, 
            Texture addButonTexture, Texture removeButtonTexture)
        {
            _addButtonTexture = addButonTexture;
            _removeButtonTexture = removeButtonTexture;

            base.Init(possibleValues, 1, true, leftButtonTexture, rightButtonTexture);
        }

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            base._Ready();
        }

        protected override ICollection<ButtonWithBackgroundControl> TransformPossibleValues(Texture[] possibleValues)
        {
            var controls = new List<ButtonWithBackgroundControl>();
            var packedScene = (PackedScene)GD.Load("res://Controls/ButtonWithBackground/ButtonWithBackgroundControl.tscn");

            for (int i = 0; i < possibleValues.Length; i++)
            {
                var control = (ButtonWithBackgroundControl)packedScene.Instance();
                control.Init(_addButtonTexture, _removeButtonTexture, i, OnSelected, OnDeselected, possibleValues[i]);
                controls.Add(control);
            }

            return controls;
        }

        public void DeselectAll()
        {
            foreach (var item in _possibleValues)
            {
                item.Deselect();
            }
        }

        public override void EnableControl()
        {
            base.EnableControl();

            foreach (var item in _contentContainer.GetChildren())
            {
                if (!(item is ButtonWithBackgroundControl control)) return;

                control.EnableControl();
            }
        }

        public override void DisableControl()
        {
            base.DisableControl();

            foreach (var item in _contentContainer.GetChildren())
            {
                if (!(item is ButtonWithBackgroundControl control)) return;
                
                control.DisableControl();
            }
        }

        protected override void OnScrollLeft()
        {
            base.OnScrollLeft();
            OnScrolled?.Invoke(this, new EventArgs());
        }

        protected override void OnScrollRight()
        {
            base.OnScrollRight();
            OnScrolled?.Invoke(this, new EventArgs());
        }
    }
}