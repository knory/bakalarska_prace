using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace Controls
{
    public abstract class SideScrollableControl<T, U> : Node2D
        where T : Node
    {
        private bool _canJumpBounds;

        protected HBoxContainer _horizontalContainer;
        protected TextureButton _leftButton;
        protected TextureButton _rightButton;
        protected HBoxContainer _contentContainer;
        protected ICollection<T> _possibleValues;
        protected int _valuesShown;
        
        /// <summary>
        /// Index of the left-most showed value.
        /// </summary>
        protected int _leftMostIndex;

        protected abstract ICollection<T> TransformPossibleValues(U[] possibleValues);

        public override void _Ready()
        {
            base._Ready();
        }

        protected void SetContent()
        {
            DeleteContent();

            var remainingValues = _possibleValues.Count - _leftMostIndex;
            var overflownValues = _valuesShown - remainingValues;
            if (overflownValues > 0)
            {
                foreach (var item in _possibleValues.Skip(_leftMostIndex))
                {
                    _contentContainer.AddChild(item);
                }

                foreach (var item in _possibleValues.Take(overflownValues))
                {
                    _contentContainer.AddChild(item);
                }
            }
            else 
            {
                foreach (var item in _possibleValues.Skip(_leftMostIndex).Take(_valuesShown))
                {
                    _contentContainer.AddChild(item);
                }
            }
        }

        public virtual void Init(U[] possibleValues, int valuesShown, bool canJumpBounds)
        {
            _canJumpBounds = canJumpBounds;
            _possibleValues = TransformPossibleValues(possibleValues);
            _valuesShown = valuesShown;
            _leftMostIndex = 0;
            
            var text1 = (Texture)GD.Load($"{Constants.SpriteNames[0]}");

            _horizontalContainer = GetNode<HBoxContainer>("HorizontalContainer");
            _leftButton = _horizontalContainer.GetNode<TextureButton>("LeftButton");
            _rightButton = _horizontalContainer.GetNode<TextureButton>("RightButton");
            _contentContainer = _horizontalContainer.GetNode<HBoxContainer>("ContentContainer");

            _leftButton.TextureNormal = text1;
            _rightButton.TextureNormal = text1;

            SetContent();

            _leftButton.Connect("pressed", this, nameof(OnScrollLeft));
            _rightButton.Connect("pressed", this, nameof(OnScrollRight));
        }

        protected virtual void OnScrollLeft()
        {
            if (_canJumpBounds)
            {
                _leftMostIndex = _leftMostIndex == 0 ? _possibleValues?.Count - 1 ?? 0 : _leftMostIndex - 1;
            }
            else
            {
                _leftMostIndex = _leftMostIndex == 0 ? 0 : _leftMostIndex - 1;
            }

            SetContent();
        }

        protected virtual void OnScrollRight()
        {
            if (_canJumpBounds)
            {
                _leftMostIndex = _leftMostIndex >= (_possibleValues?.Count - 1 ?? 0) ? 0 : _leftMostIndex + 1;
            }
            else
            {
                _leftMostIndex = _leftMostIndex >= (_possibleValues?.Count - 1 ?? 0) ? _possibleValues?.Count - 1 ?? 0 : _leftMostIndex + 1;
            }

            SetContent();
        }

        public virtual void EnableControl()
        {
            _leftButton.Disabled = false;
            _rightButton.Disabled = false;
        }

        public virtual void DisableControl()
        {
            _leftButton.Disabled = true;
            _rightButton.Disabled = true;
        }

        protected void DeleteContent()
        {
            var contentNodes = _contentContainer.GetChildren();
            foreach (var item in contentNodes)
            {
                _contentContainer.RemoveChild((Node)item);
            }
        }

        public virtual void ResetState()
        {
            _leftMostIndex = 0;
            SetContent();
        }
    }
}