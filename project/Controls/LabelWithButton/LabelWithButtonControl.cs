using Controls;
using Godot;
using System;
using Utils;

namespace Controls
{
    public class LabelWithButtonControl : Node2D
    {
        private VBoxContainer _verticalContainer;
        private Label _label;
        private ClickableControl _clickableControl;
        private string _text;
        private int _value;
        private EventHandler<SelectedValueEventArgs> _selected;
        private EventHandler<SelectedValueEventArgs> _deselected;
        
        public Texture BackgroundImage { get; private set; }

        public void Init(string text, int value, Texture backgroundImage, EventHandler<SelectedValueEventArgs> selected, EventHandler<SelectedValueEventArgs> deselected)
        {
            BackgroundImage = backgroundImage;
            _text = text;
            _value = value;
            _selected = selected;
            _deselected = deselected;
        }

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            _verticalContainer = GetNode<VBoxContainer>("VerticalContainer");
            _label = _verticalContainer.GetNode<Label>("Label");
            _clickableControl = _verticalContainer.GetNode<ClickableControl>("ClickableControl");
            
            if (string.IsNullOrEmpty(_text))
            {
                _label.Visible = false;
            }
            else
            {
                _label.Text = _text;
            }

            _clickableControl.Init(Constants.TeammateActionIcons["plus"], Constants.TeammateActionIcons["minus"], _value);

            _clickableControl.Selected += _selected;
            _clickableControl.Deselected += _deselected;
        }

        public void Deselect()
        {
            _clickableControl?.Deselect();
        }
    }
}