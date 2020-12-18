using Controls;
using Godot;
using Models;
using System;
using System.Collections.Generic;
using Utils;

namespace Controls
{
    public class SideScrollWithBackgroundControl : SideScrollableControl<LabelWithButtonControl, LabelWithButtonResource>
    {
        private TextureRect _backgroundImage;
        private EventHandler<SelectedValueEventArgs> _selectedHandler;
        private EventHandler<SelectedValueEventArgs> _deselectedHandler;

        public void Init(LabelWithButtonResource[] possibleValues, EventHandler<SelectedValueEventArgs> selected, EventHandler<SelectedValueEventArgs> deselected)
        {
            _backgroundImage = GetNode<TextureRect>("BackgroundImage");
            _selectedHandler = selected;
            _deselectedHandler = deselected;
            
            //TODO FIX
            base.Init(possibleValues, 1, true, null, null);

            SetBackgroundImage();
        }

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            //_backgroundImage = GetNode<TextureRect>("BackgroundImage");
        }

        protected override ICollection<LabelWithButtonControl> TransformPossibleValues(LabelWithButtonResource[] possibleValues)
        {
            var controls = new List<LabelWithButtonControl>();
            var packedScene = (PackedScene)ResourceLoader.Load("res://Controls/LabelWithButton/LabelWithButtonControl.tscn");

            for (int i = 0; i < possibleValues.Length; i++)
            {
                var control = (LabelWithButtonControl)packedScene.Instance();
                control.Init(possibleValues[i].Text, i, possibleValues[i].BackgroundImage, _selectedHandler, _deselectedHandler);
                controls.Add(control);
            }

            return controls;
        }

        protected override void OnScrollLeft()
        {
            base.OnScrollLeft();

            SetBackgroundImage();
        }

        protected override void OnScrollRight()
        {
            base.OnScrollRight();

            SetBackgroundImage();
        }

        public override void ResetState()
        {
            base.ResetState();

            SetBackgroundImage();
        }

        private void SetBackgroundImage()
        {
            _backgroundImage.Texture = ((List<LabelWithButtonControl>)_possibleValues)[_leftMostIndex].BackgroundImage;
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

            foreach (var item in _possibleValues)
            {
                item.EnableControl();
            }
        }

        public override void DisableControl()
        {
            base.DisableControl();

            foreach (var item in _possibleValues)
            {
                item.DisableControl();
            }
        }
    }
}